using Flowra.Backend.Application.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Flowra.Backend.WebAPI.Middlewares
{
    public sealed class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        // FIXED: Added constant for the literal "errors" string
        private const string ErrorsExtensionKey = "errors";

        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = false,
            DefaultIgnoreCondition =
                System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
        };

        public GlobalExceptionMiddleware(RequestDelegate next, IWebHostEnvironment env, ILogger<GlobalExceptionMiddleware> logger)
        {
            _next = next;
            _env = env;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path.StartsWithSegments("/swagger") || context.Request.Path.StartsWithSegments("/scalar"))
            {
                await _next(context);
                return;
            }

            var correlationId = context.Items["CorrelationId"]?.ToString();

            try
            {
                await _next(context);
            }
            catch (AppValidationException ex)
            {
                LogWarning(context, ex, "Validation error", correlationId);
                await WriteValidationProblemAsync(context, ex, correlationId);
            }
            catch (AppException ex)
            {
                LogWarning(context, ex, "Application error", correlationId);
                await WriteApplicationProblemAsync(context, ex, correlationId);
            }
            catch (Exception ex)
            {
                LogError(context, ex, correlationId);
                await WriteUnhandledProblemAsync(context, ex, correlationId);
            }
        }

        private void LogWarning(HttpContext context, Exception ex, string title, string? correlationId)
        {
            _logger.LogWarning(ex,
                "{Title} | Path: {Path} | CorrelationId: {CorrelationId}",
                title,
                context.Request.Path,
                correlationId);
        }

        private void LogError(HttpContext context, Exception ex, string? correlationId)
        {
            _logger.LogError(ex,
                "Unhandled exception | Path: {Path} | CorrelationId: {CorrelationId}",
                context.Request.Path,
                correlationId);
        }

        // FIXED: Added 'static' modifier
        private static async Task WriteValidationProblemAsync(HttpContext context, AppValidationException ex, string? correlationId)
        {
            var pd = CreateBaseProblemDetails(context, ex, correlationId);
            pd.Extensions[ErrorsExtensionKey] = ex.ValidationErrors; // FIXED: Used constant

            await WriteProblemAsync(context, pd);
        }

        // FIXED: Added 'static' modifier
        private static async Task WriteApplicationProblemAsync(HttpContext context, AppException ex, string? correlationId)
        {
            var pd = CreateBaseProblemDetails(context, ex, correlationId);

            if (ex is BusinessRuleException busEx &&
                !string.IsNullOrEmpty(busEx.PropertyName))
            {
                pd.Extensions[ErrorsExtensionKey] = new Dictionary<string, string[]> // FIXED: Used constant
                {
                    { busEx.PropertyName, new[] { ex.Detail } }
                };
            }
            else if (ex is ConflictException confEx &&
                     !string.IsNullOrEmpty(confEx.PropertyName))
            {
                pd.Extensions[ErrorsExtensionKey] = new Dictionary<string, string[]> // FIXED: Used constant
                {
                    { confEx.PropertyName, new[] { ex.Detail } }
                };
            }
            else if (ex.Errors is not null && ex.Errors.Any())
            {
                pd.Extensions[ErrorsExtensionKey] = new Dictionary<string, IEnumerable<string>> // FIXED: Used constant
                {
                    { "General", ex.Errors }
                };
            }

            await WriteProblemAsync(context, pd);
        }

        private async Task WriteUnhandledProblemAsync(HttpContext context, Exception ex, string? correlationId)
        {
            var pd = new ProblemDetails
            {
                Title = "Internal Server Error",
                Detail = "Beklenmeyen bir hata oluştu.",
                Type = "https://euro-scada.com/errors/internal",
                Status = StatusCodes.Status500InternalServerError,
                Instance = context.Request.Path
            };

            AddCommonExtensions(context, pd, correlationId);

            if (_env.IsDevelopment())
            {
                pd.Extensions["exceptionMessage"] = ex.Message;
                pd.Extensions["stackTrace"] = ex.StackTrace;
            }

            await WriteProblemAsync(context, pd);
        }

        private static ProblemDetails CreateBaseProblemDetails(HttpContext context, AppException ex, string? correlationId)
        {
            var pd = new ProblemDetails
            {
                Title = ex.Title,
                Detail = ex.Detail,
                Type = ex.TypeUri,
                Status = ex.Status,
                Instance = context.Request.Path
            };

            AddCommonExtensions(context, pd, correlationId);
            return pd;
        }

        private static void AddCommonExtensions(HttpContext context, ProblemDetails pd, string? correlationId)
        {
            pd.Extensions["method"] = context.Request.Method;
            pd.Extensions["path"] = context.Request.Path.ToString();

            if (!string.IsNullOrWhiteSpace(correlationId))
                pd.Extensions["correlationId"] = correlationId;

            var activity = System.Diagnostics.Activity.Current;
            if (activity is not null)
            {
                pd.Extensions["traceId"] = activity.TraceId.ToString();
                pd.Extensions["spanId"] = activity.SpanId.ToString();
            }
        }

        private static async Task WriteProblemAsync(HttpContext context, ProblemDetails problemDetails)
        {
            if (context.Response.HasStarted)
                return;

            context.Response.StatusCode =
                problemDetails.Status ?? StatusCodes.Status500InternalServerError;

            context.Response.ContentType = "application/problem+json";

            var json = JsonSerializer.Serialize(problemDetails, JsonOptions);
            await context.Response.WriteAsync(json);
        }
    }
}

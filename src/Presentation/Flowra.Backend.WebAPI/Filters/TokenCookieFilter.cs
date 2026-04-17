using Flowra.Backend.Application.Common.Responses;
using Flowra.Backend.Application.DTOs.Auth;
using Flowra.Backend.WebAPI.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;

namespace Flowra.Backend.WebAPI.Filters
{
    public sealed class TokenCookieFilter : IAsyncResultFilter
    {
        private readonly JwtCookieOptions _cookieOptions;

        public TokenCookieFilter(IOptions<JwtCookieOptions> cookieOptions)
        {
            _cookieOptions = cookieOptions.Value;
        }

        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            if (context.Result is ObjectResult { Value: not null } objectResult &&
                objectResult.Value is SuccessDetails<AuthResultDto> authResponse)
            {
                WriteAuthCookies(context.HttpContext, authResponse);

                if (authResponse.Data != null && authResponse.Data.UserId == 0)
                {
                    var cleanResponse = new SuccessDetails
                    {
                        Type = authResponse.Type,
                        Title = authResponse.Title,
                        Status = authResponse.Status,
                        Detail = authResponse.Detail,
                        Meta = authResponse.Meta
                    };

                    context.Result = new ObjectResult(cleanResponse)
                    {
                        StatusCode = objectResult.StatusCode
                    };
                }
            }

            if (ShouldClearCookies(context))
            {
                ClearCookies(context.HttpContext);
            }

            await next();
        }

        private void WriteAuthCookies(HttpContext httpContext, SuccessDetails<AuthResultDto> response)
        {
            var dto = response.Data;
            if (dto is null || dto.RequiresPasswordReset)
                return;

            if (!string.IsNullOrWhiteSpace(dto.AccessToken))
            {
                httpContext.Response.Cookies.Append(
                    JwtCookieNames.AccessToken,
                    dto.AccessToken,
                    CreateCookieOptions(dto.AccessTokenExpiresAtUtc, _cookieOptions.AccessTokenPath));
            }

            if (!string.IsNullOrWhiteSpace(dto.RefreshToken))
            {
                httpContext.Response.Cookies.Append(
                    JwtCookieNames.RefreshToken,
                    dto.RefreshToken,
                    CreateCookieOptions(dto.RefreshTokenExpiresAtUtc, _cookieOptions.RefreshTokenPath));
            }

            dto.AccessToken = string.Empty;
            dto.RefreshToken = string.Empty;
            dto.RefreshTokenExpiresAtUtc = null;
            dto.AccessTokenExpiresAtUtc = null;
        }

        private static bool ShouldClearCookies(ResultExecutingContext context)
        {
            if (context.Result is not ObjectResult and not StatusCodeResult and not EmptyResult)
                return false;

            var action = context.ActionDescriptor.RouteValues.TryGetValue("action", out var actionName)
                ? actionName
                : null;

            var controller = context.ActionDescriptor.RouteValues.TryGetValue("controller", out var controllerName)
                ? controllerName
                : null;

            return string.Equals(controller, "Auth", StringComparison.OrdinalIgnoreCase)
                   && string.Equals(action, "Logout", StringComparison.OrdinalIgnoreCase);
        }

        private void ClearCookies(HttpContext httpContext)
        {
            httpContext.Response.Cookies.Delete(
                JwtCookieNames.AccessToken,
                CreateDeleteCookieOptions(_cookieOptions.AccessTokenPath));

            httpContext.Response.Cookies.Delete(
                JwtCookieNames.RefreshToken,
                CreateDeleteCookieOptions(_cookieOptions.RefreshTokenPath));
        }

        private CookieOptions CreateCookieOptions(DateTime? expiresUtc, string path)
        {
            return new CookieOptions
            {
                HttpOnly = _cookieOptions.HttpOnly,
                Secure = _cookieOptions.Secure,
                SameSite = _cookieOptions.SameSite,
                Expires = expiresUtc,
                Path = path,
                IsEssential = _cookieOptions.IsEssential
            };
        }

        private CookieOptions CreateDeleteCookieOptions(string path)
        {
            return new CookieOptions
            {
                HttpOnly = _cookieOptions.HttpOnly,
                Secure = _cookieOptions.Secure,
                SameSite = _cookieOptions.SameSite,
                Path = path,
                IsEssential = _cookieOptions.IsEssential
            };
        }
    }
}
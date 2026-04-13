using Flowra.Backend.Application.Common.Responses;
using Flowra.Backend.Infrastructure.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace Flowra.Backend.WebAPI.Authentication
{
    public static class JwtAuthenticationExtensions
    {
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtCookieOptions>(configuration.GetSection("JwtCookieOptions"));

            var jwt = configuration.GetSection(JwtSettings.SectionName).Get<JwtSettings>()
                ?? throw new InvalidOperationException("JwtSettings section is missing.");

            Validate(jwt);

            var signingKey = new SymmetricSecurityKey(Convert.FromBase64String(jwt.SigningKey));

            SymmetricSecurityKey? decryptionKey = null;
            if (!string.IsNullOrWhiteSpace(jwt.EncryptionKey))
            {
                decryptionKey = new SymmetricSecurityKey(
                    Convert.FromBase64String(jwt.EncryptionKey));
            }

            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = jwt.RequireHttpsMetadata;
                    options.SaveToken = false;

                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            if (string.IsNullOrWhiteSpace(context.Token) &&
                                context.Request.Cookies.TryGetValue(
                                    JwtCookieNames.AccessToken,
                                    out var cookieToken) &&
                                !string.IsNullOrWhiteSpace(cookieToken))
                            {
                                context.Token = cookieToken;
                            }

                            return Task.CompletedTask;
                        },

                        OnChallenge = async context =>
                        {
                            context.HandleResponse();

                            if (!context.Response.HasStarted)
                            {
                                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                                context.Response.ContentType = "application/problem+json";

                                var problem = new ProblemDetails
                                {
                                    Type = "https://flowra.com/errors/unauthorized",
                                    Title = "Unauthorized",
                                    Status = StatusCodes.Status401Unauthorized,
                                    Detail = "Yetkilendirme başarısız. Token eksik, süresi dolmuş veya geçersiz.",
                                    CorrelationId = context.HttpContext.TraceIdentifier
                                };

                                await context.Response.WriteAsJsonAsync(problem);
                            }
                        },

                        OnForbidden = async context =>
                        {
                            if (!context.Response.HasStarted)
                            {
                                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                                context.Response.ContentType = "application/problem+json";

                                var problem = new ProblemDetails
                                {
                                    Type = "https://flowra-scada.com/errors/forbidden",
                                    Title = "Forbidden",
                                    Status = StatusCodes.Status403Forbidden,
                                    Detail = "Bu işlem için yetkiniz yok.",
                                    CorrelationId = context.HttpContext.TraceIdentifier
                                };

                                await context.Response.WriteAsJsonAsync(problem);
                            }
                        },

                        OnAuthenticationFailed = context =>
                        {
                            var logger = context.HttpContext.RequestServices
                                .GetRequiredService<ILoggerFactory>()
                                .CreateLogger("JwtAuthentication");

                            logger.LogWarning(
                                context.Exception,
                                "JWT authentication failed. Path: {Path}, TraceId: {TraceId}",
                                context.HttpContext.Request.Path,
                                context.HttpContext.TraceIdentifier);

                            return Task.CompletedTask;
                        }
                    };

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = jwt.Issuer,
                        ValidAudience = jwt.Audience,
                        IssuerSigningKey = signingKey,
                        TokenDecryptionKey = decryptionKey,

                        ClockSkew = TimeSpan.Zero,

                        NameClaimType = ClaimTypes.Name,
                        RoleClaimType = ClaimTypes.Role
                    };
                });

            services.AddAuthorization();

            return services;
        }

        private static void Validate(JwtSettings jwt)
        {
            if (string.IsNullOrWhiteSpace(jwt.Issuer))
                throw new InvalidOperationException("JwtSettings:Issuer is missing.");

            if (string.IsNullOrWhiteSpace(jwt.Audience))
                throw new InvalidOperationException("JwtSettings:Audience is missing.");

            if (string.IsNullOrWhiteSpace(jwt.SigningKey))
                throw new InvalidOperationException("JwtSettings:SigningKey is missing.");

            try
            {
                var signingKeyBytes = Convert.FromBase64String(jwt.SigningKey);

                if (signingKeyBytes.Length < 32)
                    throw new InvalidOperationException("JwtSettings:SigningKey must be at least 32 bytes.");
            }
            catch (FormatException ex)
            {
                throw new InvalidOperationException(
                    "JwtSettings:SigningKey must be a valid Base64 string.",
                    ex);
            }

            if (!string.IsNullOrWhiteSpace(jwt.EncryptionKey))
            {
                try
                {
                    var encryptionKeyBytes = Convert.FromBase64String(jwt.EncryptionKey);

                    if (encryptionKeyBytes.Length < 32)
                        throw new InvalidOperationException("JwtSettings:EncryptionKey must be at least 32 bytes.");
                }
                catch (FormatException ex)
                {
                    throw new InvalidOperationException(
                        "JwtSettings:EncryptionKey must be a valid Base64 string.",
                        ex);
                }
            }

            if (jwt.AccessTokenExpirationMinutes <= 0)
                throw new InvalidOperationException("JwtSettings:AccessTokenExpirationMinutes must be greater than zero.");

            if (jwt.RefreshTokenExpirationDays <= 0)
                throw new InvalidOperationException("JwtSettings:RefreshTokenExpirationDays must be greater than zero.");
        }
    }
}
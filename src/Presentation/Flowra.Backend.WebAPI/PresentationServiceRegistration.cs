using Flowra.Backend.Application.Abstractions.Presentation;
using Flowra.Backend.Presentation.Abstractions;
using Flowra.Backend.WebAPI.Authentication;
using Flowra.Backend.WebAPI.Filters;
using Flowra.Backend.WebAPI.RequestContext;
using Flowra.Backend.WebAPI.Services;

namespace Flowra.Backend.WebAPI
{
    public static class PresentationServiceRegistration
    {
        public static IServiceCollection AddPresentationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpContextAccessor();
            services.AddScoped<IRequestContext, HttpRequestContext>();
            services.AddScoped<ITokenCookieService, TokenCookieService>();
            services.AddScoped<TokenCookieFilter>();

            services.AddControllers();

            // JWT Authentication
            services.AddJwtAuthentication(configuration);

            return services;
        }
    }
}

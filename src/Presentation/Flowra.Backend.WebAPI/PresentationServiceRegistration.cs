using Flowra.Backend.Application.Abstractions.Presentation;
using Flowra.Backend.WebAPI.RequestContext;

namespace Flowra.Backend.WebAPI
{
    public static class PresentationServiceRegistration
    {
        public static IServiceCollection AddPresentationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpContextAccessor();
            services.AddScoped<IRequestContext, HttpRequestContext>();

            return services;
        }
    }
}

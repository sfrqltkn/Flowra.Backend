using Flowra.Backend.Application.Common.Behaviors;
using Flowra.Backend.Application.Services;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Flowra.Backend.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            var assembly = typeof(ApplicationServiceRegistration).Assembly;

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(assembly);
                cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
            });

            services.AddValidatorsFromAssembly(assembly);

            services.AddHttpClient("CollectApi", client =>
            {
                var baseUrl = configuration["CollectApi:BaseUrl"];
                var apiKey = configuration["CollectApi:ApiKey"];
                client.BaseAddress = new Uri(baseUrl!);

                client.DefaultRequestHeaders.Add("authorization", apiKey);
            });

            services.AddHttpClient<IAiAdvisorService, AiAdvisorService>();

            return services;
        }
    }
}
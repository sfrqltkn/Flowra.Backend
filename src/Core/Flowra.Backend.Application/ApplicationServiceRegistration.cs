using Flowra.Backend.Application.Common.Behaviors;
using Flowra.Backend.Application.Interfaces.Services;
using Flowra.Backend.Application.Services;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;


namespace Flowra.Backend.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            var assembly = typeof(ApplicationServiceRegistration).Assembly;

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(assembly);
            });

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddValidatorsFromAssembly(assembly);

            // İş Mantığı Servisleri
            services.AddScoped<IExpenseService, ExpenseService>();
            services.AddScoped<IIncomeService, IncomeService>();
            services.AddScoped<IAssetService, AssetService>();
            services.AddScoped<ICashRecordService, CashRecordService>();
            services.AddScoped<IAllowanceService, AllowanceService>();

            // External API Servisleri (HttpClient)
            services.AddHttpClient<IFinanceDataService, FinanceDataService>(client =>
            {
                client.BaseAddress = new Uri("https://api.collectapi.com/");
            });
            services.AddHttpClient<IAiAdvisorService, AiAdvisorService>();

            return services;
        }
    }
}

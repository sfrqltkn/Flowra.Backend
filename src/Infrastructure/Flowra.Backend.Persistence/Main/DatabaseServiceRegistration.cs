using Flowra.Backend.Persistence.Interceptors;
using Flowra.Backend.Persistence.Main.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Flowra.Backend.Persistence.Main
{
    public static class DatabaseServiceRegistration
    {
        public static IServiceCollection AddPostgreSql(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString =
                configuration.GetConnectionString("PostgreSQL");
            // Interceptor DI kaydı
            services.AddScoped<AuditTrackableInterceptor>();

            services.AddDbContext<FlowraDbContext>((sp, options) =>
            {
                options.UseNpgsql(connectionString, npgsqlOptions =>
                {
                    npgsqlOptions.MigrationsAssembly(
                        typeof(FlowraDbContext).Assembly.FullName);

                    npgsqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(10),
                        errorCodesToAdd: null);
                });
            });

            return services;
        }
    }
}

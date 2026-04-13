using Flowra.Backend.Persistence.Main.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Flowra.Backend.Persistence.Main
{
    public static class MainMigrationExtensions
    {
        public static void ApplyMainMigrations(this IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<FlowraDbContext>>();
            var dbContext = scope.ServiceProvider.GetRequiredService<FlowraDbContext>();

            try
            {
                logger.LogInformation("FlowraDbContext migration kontrolü yapılıyor...");
                dbContext.Database.Migrate();
                logger.LogInformation("FlowraDbContext güncel.");
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("FlowraDbContext migration işlemi sırasında hata oluştu.", ex);
            }
        }
    }
}
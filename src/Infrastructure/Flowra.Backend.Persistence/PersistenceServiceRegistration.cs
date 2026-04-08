using Flowra.Backend.Persistence.Main.Repositories;
using Flowra.Backend.Application.Abstractions.Persistence;
using Flowra.Backend.Application.Abstractions.Persistence.Repositories.Identity;
using Flowra.Backend.Application.Interfaces.Repositories;
using Flowra.Backend.Persistence.Main;
using Flowra.Backend.Persistence.Main.UnitOfWork;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Flowra.Backend.Persistence.Main.Repositories.Identity;

namespace Flowra.Backend.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Database
            services.AddPostgreSql(configuration);

            // Identity
            services.AddIdentityConfiguration();

            // Repositories
            services.AddScoped(typeof(IReadRepository<,>), typeof(EfReadRepository<,>));
            services.AddScoped(typeof(IWriteRepository<,>), typeof(EfWriteRepository<,>));

            // Unit Of Work
            services.AddScoped<IUnitOfWork, EfUnitOfWork>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserRolesRepository, UserRolesRepository>();

            // Repositories
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            return services;
        }

    }
}

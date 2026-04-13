using Flowra.Backend.Application.Abstractions.Infrastructure;
using Flowra.Backend.Application.Abstractions.Infrastructure.Services.Mail;
using Flowra.Backend.Application.Abstractions.Infrastructure.Token;
using Flowra.Backend.Infrastructure.Services.Identity;
using Flowra.Backend.Infrastructure.Services.Mail;
using Flowra.Backend.Infrastructure.Services.Token;
using Flowra.Backend.Infrastructure.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Flowra.Backend.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            //appsettings.json içindeki JwtSettings bölümünü JwtSettings sınıfına bağlar
            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
            services.AddScoped<ITokenService, TokenService>();

            services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
            services.AddScoped<IMailService, MailService>();
            services.AddScoped<IMailSender, SmtpMailSender>();
            services.AddScoped<ITemplateService, TemplateService>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();

            return services;
        }
    }
}

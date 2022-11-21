using Application.Common.Interfaces;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repository;
using Infrastructure.Persistence.Services;
using Microsoft.Extensions.Configuration;
using Web.Middlewares;
using Web.Utils.Settings;
using static System.Collections.Specialized.BitVector32;

namespace Web
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddWebUIServices(this IServiceCollection services, IConfiguration configuration)
        {

            AppSettings appSettings = configuration.GetSection("AppSettings").Get<AppSettings>();

            services.AddScoped<IAppSettings>((serviceProvider) =>
            {
                return configuration.GetSection("AppSettings").Get<AppSettings>();
            });

            services.AdicionarAuthentication(appSettings);

            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
            services.AddScoped<ITokenServices, TokenService>();
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IDocumentTypeRepository, DocumentTypeRepository>();



            services.AddHttpContextAccessor();
            return services;
        }
    }
}

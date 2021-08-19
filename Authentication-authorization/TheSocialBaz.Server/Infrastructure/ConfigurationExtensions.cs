using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TheSocialBaz.Server.Infrastructure
{
    public static class ConfigurationExtensions
    {
        public static string GetDefaultConnectionString(this IConfiguration configuration)
            => configuration.GetConnectionString("DefaultConnection");

        public static AppSettings GetAppSettings(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var appSettingsConfiguration = configuration.GetSection("ApplicationSettings");
            services.Configure<AppSettings>(appSettingsConfiguration);
            return appSettingsConfiguration.Get<AppSettings>();
        }
        
    }
}

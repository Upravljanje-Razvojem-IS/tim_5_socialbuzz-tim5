using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIGateway.Infrastructure
{
    public static class ConfigurationExtensions
    {
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

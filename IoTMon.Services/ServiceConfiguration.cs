using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using IoTMon.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using IoTMon.Services.Notifiers;
using IoTMon.Models.Notifiers;

namespace IoTMon.Services
{
    public static class ServiceConfiguration
    {
        public static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            var authParams = configuration.GetSection("Jwt")
                .Get<AuthParameters>();

            EmailNotifyConfig emailConfig = configuration.GetSection("Notifiers")
                .GetSection("Sendgrid")
                .Get<EmailNotifyConfig>();

            services.AddSingleton<IAlertNotifier>(a => new EmailNotifier(emailConfig));
            services.AddScoped<IAuthService>(a => new AuthService(authParams));
            services.AddSingleton<IDataParser, DataParser>();
        }
    }
}

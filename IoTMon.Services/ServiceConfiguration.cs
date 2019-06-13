using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using IoTMon.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace IoTMon.Services
{
    public static class ServiceConfiguration
    {
        public static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            var authParams = configuration.GetSection("Jwt").Get<AuthParameters>();


            services.AddScoped<IAuthService>(a => new AuthService(authParams));
        }
    }
}

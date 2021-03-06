﻿using IoTMon.DataServices.Contracts;
using IoTMon.Models.TimeSeries;
using IoTMon.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace IoTMon.DataServices
{
    public static class ServiceConfiguration
    {
        public static void AddDataServices(this IServiceCollection services, IConfiguration configuration)
        {
            var influxDbConfig = configuration.GetSection("InfluxDb")
                .Get<InfluxDbConfig>();

            services.AddScoped<ITimeSeriesProvider>(ts => new InfluxDbProvider(influxDbConfig));
            services.AddScoped<IDeviceService, DeviceService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IDataManager, DataManager>();
            services.AddTransient<IAlertService, AlertService>();
        }
    }
}

using IoTMon.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System;
using System.IO;

namespace IoTMon.DataSimulator
{
    // read devices from sql srv
    // built devices/sensors from the db
    // iterate over sensors and set timer and work
    // generate random value of that type
    // rabbitMQ connection
    // send message in format:
    // timestamp | device_id | sensor | value

    class Simulator
    {
        private static IServiceProvider serviceProvider;
        private static IConfiguration Configuration;
        
        static void Main(string[] args)
        {
            Configure();
            RegisterServices();



            DisposeServices();
        }

        private static void RegisterServices()
        {
            var services = new ServiceCollection();

            services.AddDbContext<ApplicationDbContext>(options =>
                          options.UseSqlServer(Configuration.GetConnectionString("IoTMonitoring")));



            serviceProvider = services.BuildServiceProvider();
        }

        private static void Configure()
        {
            var relativePath = @"../../../../IoTMon.WebApi";
            var absolutePath = Path.GetFullPath(relativePath);
            var fileProvider = new PhysicalFileProvider(absolutePath);

            Configuration = new ConfigurationBuilder()
                  .AddJsonFile(fileProvider, "appsettings.json", false, true)
                  .Build();
        }
        
        private static void DisposeServices()
        {
            if (serviceProvider == null)
            {
                return;
            }
            if (serviceProvider is IDisposable)
            {
                ((IDisposable)serviceProvider).Dispose();
            }
        }
    }
}

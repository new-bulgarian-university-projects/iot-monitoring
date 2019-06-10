using IoTMon.Data;
using IoTMon.DataServices;
using IoTMon.DataServices.Contracts;
using IoTMon.Models.AMQP;
using IoTMon.Models.Enums;
using IoTMon.Services;
using IoTMon.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System;
using System.IO;
using System.Threading;

namespace IoTMon.DataSimulator
{
    // DONE read devices from sql srv 
    // DONE built devices/sensors from the db
    // rabbitMQ connection
    // iterate over sensors and set timer and work
    
    // send message in format:
    // timestamp | device_id | sensor | value

    class Simulator
    {
        private static IServiceProvider serviceProvider;
        private static IConfiguration Configuration;

        private static IDeviceService deviceService;
        private static ISimulatorHelpers helpers;

        static void Main(string[] args)
        {
            Configure();
            RegisterServices();

            deviceService = serviceProvider.GetService<IDeviceService>();
            helpers = serviceProvider.GetService<ISimulatorHelpers>();

            var devices = deviceService.GetDevices();

            foreach (var device in devices)
            {
                foreach (var sensor in device.Sensors)
                {
                    TimerState state = new TimerState(device, sensor);
                    var timer = new Timer(Execute, state, 0, device.IntervalInSeconds * 1000);
                }
            }

            Console.ReadLine();
            DisposeServices();
        }

        private static void Execute(object objectState)
        {
            TimerState state = (TimerState)objectState;

            var message = new Message()
            {
                Timestamp = helpers.GetDatetimeUTC(),
                DeviceId = state.Device.Id,
                SensorLabel = state.Sensor.Label,
                Value = helpers.GetRandomNumber(state.Sensor.Label),
                ValueType = state.Sensor.ValueType.ToString()

            };

            Console.WriteLine(message);
        }

        private static void RegisterServices()
        {
            var services = new ServiceCollection();

            services.AddDbContext<ApplicationDbContext>(options =>
                          options.UseSqlServer(Configuration.GetConnectionString("IoTMonitoring")));

            services.AddTransient<IDeviceService, DeviceService>();
            services.AddSingleton<ISimulatorHelpers, SimulatorHelper>();


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
                Console.WriteLine("Disposing ... ");
                ((IDisposable)serviceProvider).Dispose();
            }
        }
    }
}

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
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace IoTMon.DataSimulator
{
    class Simulator
    {
        private static IServiceProvider serviceProvider;
        private static IConfiguration Configuration;

        private static IDeviceService deviceService;
        private static ISimulatorHelpers helpers;

        private static IModel channel;
        private static RabbitMQConfig rabbitMQConfig;
        private static List<Timer> timers = new List<Timer>();

        static void Main(string[] args)
        {
            Configure();
            RegisterServices();

            deviceService = serviceProvider.GetService<IDeviceService>();
            helpers = serviceProvider.GetService<ISimulatorHelpers>();


            if (rabbitMQConfig == null)
            {
                throw new ArgumentNullException("RabbitMQ");
            }

            var devices = deviceService.GetDevices();

            var factory = new ConnectionFactory()
            {
                HostName = rabbitMQConfig.HostName,
                UserName = rabbitMQConfig.UserName,
                Password = rabbitMQConfig.Password
            };

            using (var connection = factory.CreateConnection())
            using (channel = connection.CreateModel())
            {
                channel.QueueDeclare(rabbitMQConfig.QueueName, true, false, false, null);

                foreach (var device in devices)
                {
                    foreach (var sensor in device.Sensors)
                    {
                        TimerState state = new TimerState(device, sensor);
                        var timer = new Timer(Execute, state, 0, device.IntervalInSeconds * 1000);

                        timers.Add(timer);
                    }
                }
                Console.ReadLine();
            }
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

            var body = Utils.Serialize(message);

            channel.BasicPublish(exchange: "",
                                 routingKey: rabbitMQConfig.QueueName,
                                 basicProperties: null,
                                 body: body);

            Console.WriteLine(" [x] Sent {0}", message);
        }

        private static void RegisterServices()
        {
            var services = new ServiceCollection();
            services.AddDbContext<ApplicationDbContext>(options =>
                          options.UseSqlServer(Configuration.GetConnectionString("IoTMonitoring")));

            services.AddTransient<IDeviceService, DeviceService>();
            services.AddSingleton<ISimulatorHelpers, SimulatorHelper>();
            services.AddSingleton<Utils>();

            rabbitMQConfig = Configuration.GetSection("RabbitMQ").Get<RabbitMQConfig>();

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

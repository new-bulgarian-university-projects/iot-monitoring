﻿using IoTMon.Data;
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
using System.Linq;
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
        private static Timer updateDevices;

        static void Main(string[] args)
        {
            Configuration = Utils.Configure();
            RegisterServices();

            deviceService = serviceProvider.GetService<IDeviceService>();
            helpers = serviceProvider.GetService<ISimulatorHelpers>();


            if (rabbitMQConfig == null)
            {
                throw new ArgumentNullException("RabbitMQ");
            }



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

                updateDevices = new Timer((_) =>
                {
                    timers.ForEach(t => t.Dispose());
                    timers.Clear();
                    var devices = deviceService.GetDevices();
                    foreach (var device in devices)
                    {
                        foreach (var sensor in device.Sensors)
                        {
                            TimerState state = new TimerState(device, sensor);
                            var timer = new Timer(Execute, state, 0, device.IntervalInSeconds * 1000);

                            timers.Add(timer);
                        }
                    }
                    Console.WriteLine("---- Devices Updated ----");
                }, null, 0, 30 * 1000);

                Console.ReadLine();
            }
            DisposeServices();
        }

        private static void Execute(object objectState)
        {
            TimerState state = (TimerState)objectState;

            var message = new Message()
            {
                Time = DateTime.Now,
                DeviceId = state.Device.Id.ToString(),
                Sensor = state.Sensor.Label,
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


            rabbitMQConfig = Configuration.GetSection("RabbitMQ").Get<RabbitMQConfig>();

            serviceProvider = services.BuildServiceProvider();
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

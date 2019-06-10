using IoTMon.DataServices;
using IoTMon.DataServices.Contracts;
using IoTMon.Models.AMQP;
using IoTMon.Services;
using IoTMon.Services.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.IO;
using System.Text;

namespace IoTMon.IngestionWorker
{
    class IngestionWorker
    {
        // listen rabbitmq 
        // receive message
        // parse it
        // open influxdb connection
        // write the point

        private static IServiceProvider serviceProvider;
        private static IConfiguration Configuration;
        private static RabbitMQConfig rabbitMQConfig;

        static void Main(string[] args)
        {
            Configure();
            RegisterServices();

            var factory = new ConnectionFactory()
            {
                HostName = rabbitMQConfig.HostName,
                UserName = rabbitMQConfig.UserName,
                Password = rabbitMQConfig.Password
            };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(rabbitMQConfig.QueueName, true, false, false, null);
                channel.BasicQos(0, 2, false);

                var consumer = new EventingBasicConsumer(channel);

                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var message = Utils.Deserialize<Message>(body);
                    Console.WriteLine(" [x] Received {0}", message);
                    channel.BasicAck(ea.DeliveryTag, false);
                };

                channel.BasicConsume(rabbitMQConfig.QueueName, false, consumer);

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }

            DisposeServices();
        }

        private static void Configure()
        {
            var relativePath = @"../../../../../IoTMon.WebApi";
            var absolutePath = Path.GetFullPath(relativePath);
            var fileProvider = new PhysicalFileProvider(absolutePath);

            Configuration = new ConfigurationBuilder()
                  .AddJsonFile(fileProvider, "appsettings.json", false, true)
                  .Build();
        }

        private static void RegisterServices()
        {
            var services = new ServiceCollection();

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

using IoTMon.DataServices;
using IoTMon.DataServices.Contracts;
using IoTMon.Models.AMQP;
using IoTMon.Models.TimeSeries;
using IoTMon.Services;
using IoTMon.Services.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace IoTMon.IngestionWorker
{
    class IngestionWorker
    {
        private static IServiceProvider serviceProvider;
        private static IConfiguration Configuration;
        private static RabbitMQConfig rabbitMQConfig;

        private static ITimeSeriesProvider influxDb;
        private static ushort UnackedMessagesPerConsumer = 15;

        static void Main(string[] args)
        {
            Configuration = Utils.Configure();
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
                channel.BasicQos(0, UnackedMessagesPerConsumer, false);

                var consumer = new EventingBasicConsumer(channel);

                consumer.Received += async (model, ea) =>
                {
                    var body = ea.Body;
                    var message = Utils.Deserialize<Message>(body);
                    Console.WriteLine(" [x] Received {0}", message);

                    // build a point and save to Influx
                    Measurement point = BuildPoint(message);
                    await influxDb.WriteAsync(point);

                    channel.BasicAck(ea.DeliveryTag, false);
                };

                channel.BasicConsume(rabbitMQConfig.QueueName, false, consumer);


                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }

            DisposeServices();
        }

        private static Measurement BuildPoint(Message message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            Measurement point = new Measurement()
            {
                MeasurementName = "sensors",
                Timestamp = message.Timestamp,
                Tags = new Dictionary<string, object>
                        {
                            { "deviceId", message.DeviceId },
                            { "sensor", message.SensorLabel }
                        },
                Fields = new Dictionary<string, object>
                        {
                            { "value", message.Value.ToString()},
                            { "valueType", message.ValueType}
                        }
            };

            return point;
        }
        private static void RegisterServices()
        {
            var services = new ServiceCollection();
            services.AddDataServices();
            services.AddSingleton<ISimulatorHelpers, SimulatorHelper>();

            rabbitMQConfig = Configuration.GetSection("RabbitMQ").Get<RabbitMQConfig>();
            serviceProvider = services.BuildServiceProvider();

            influxDb = serviceProvider.GetService<ITimeSeriesProvider>();
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

using System;
using System.Collections.Generic;
using System.Text;

namespace IoTMon.Models.AMQP
{
    public class RabbitMQConfig
    {
        public string HostName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string QueueName { get; set; }
    }
}

using IoTMon.Models.AMQP;
using IoTMon.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace IoTMon.Services
{
    public class DeviceService : IDeviceService
    {
        public DeviceService()
        {

        }
        public IEnumerable<Message> GetSensorData(string deviceId, string sensor)
        {
            throw new NotImplementedException();
        }
    }
}

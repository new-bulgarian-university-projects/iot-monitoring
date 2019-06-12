using IoTMon.Models.AMQP;
using System;
using System.Collections.Generic;
using System.Text;

namespace IoTMon.Services.Contracts
{
    public interface IDeviceService
    {
        IEnumerable<Message> GetSensorData(string deviceId, string sensor);
    }
}

using IoTMon.Models.AMQP;
using IoTMon.Models.TimeSeries;
using System;
using System.Collections.Generic;
using System.Text;

namespace IoTMon.Services.Contracts
{
    public interface IDataParser
    {
        IEnumerable<ChartData> ParseMessages(string sensor, IEnumerable<Message> incoming);
    }
}

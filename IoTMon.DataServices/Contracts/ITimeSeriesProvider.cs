using IoTMon.Models.TimeSeries;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IoTMon.DataServices.Contracts
{
    public interface ITimeSeriesProvider
    {
        Task WriteAsync(PointMeasure data);
        Task WriteAsync(IEnumerable<PointMeasure> data);
        Task<IEnumerable<Models.AMQP.Message>> QueryAsync(Guid deviceId, string sensor, DateTime? from = null);
    }
}

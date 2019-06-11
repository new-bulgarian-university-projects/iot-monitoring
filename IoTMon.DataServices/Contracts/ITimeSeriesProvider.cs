using IoTMon.Models.TimeSeries;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IoTMon.DataServices.Contracts
{
    public interface ITimeSeriesProvider
    {
        Task WriteAsync(Measurement data);
        Task WriteAsync(IEnumerable<Measurement> data);
    }
}

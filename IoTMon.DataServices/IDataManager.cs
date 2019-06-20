using IoTMon.Models.SignalR;
using IoTMon.Models.TimeSeries;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IoTMon.DataServices
{
    public interface IDataManager
    {
        Task<IEnumerable<ChartData>> Get(SignalRFilter filter);
    }
}

using IoTMon.Models.AMQP;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IoTMon.DataServices.Contracts
{
    public interface IAlertService : IDisposable
    {
        Task CheckAlerts(Message message);
    }
}

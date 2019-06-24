using IoTMon.Models.AMQP;
using System;
using System.Collections.Generic;
using System.Text;

namespace IoTMon.DataServices.Contracts
{
    public interface IAlertService
    {
        void CheckAlerts(Message message);
    }
}

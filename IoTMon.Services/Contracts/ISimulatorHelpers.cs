using IoTMon.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace IoTMon.Services.Contracts
{
    public interface ISimulatorHelpers
    {
        object GetRandomNumber(string sensor);
        DateTime GetDatetimeUTC ();
    }
}

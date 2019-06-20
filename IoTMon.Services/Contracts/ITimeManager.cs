using System;
using System.Collections.Generic;
using System.Text;

namespace IoTMon.Services.Contracts
{
    public interface ITimeManager
    {
        void ExecuteAsync(object stateInfo);
    }
}

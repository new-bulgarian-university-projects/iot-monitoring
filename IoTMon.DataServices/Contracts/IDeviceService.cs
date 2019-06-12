using IoTMon.Models.DTO;
using IoTMon.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace IoTMon.DataServices.Contracts
{
    public interface IDeviceDataService
    {
        IEnumerable<DeviceDTO> GetDevices(StatusEnum status = StatusEnum.All, ScopeEnum scope = ScopeEnum.All);
    }
}

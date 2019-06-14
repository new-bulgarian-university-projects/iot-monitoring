using IoTMon.Models.DTO;
using IoTMon.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace IoTMon.DataServices.Contracts
{
    public interface IDeviceService
    {
        IEnumerable<DeviceDTO> GetDevices(StatusEnum status = StatusEnum.All, ScopeEnum scope = ScopeEnum.All);
        IEnumerable<SensorDTO> GetSensors();
        DeviceDTO CreateDevice(DeviceDTO device);
    }
}

using IoTMon.Models.DTO;
using IoTMon.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace IoTMon.DataServices.Contracts
{
    public interface IDeviceService
    {
        IEnumerable<DeviceDTO> GetDevices(
            StatusEnum status = StatusEnum.All,
            ScopeEnum scope = ScopeEnum.All,
            Guid? userId = null);
        DeviceDTO GetDeviceById(Guid deviceId);
        IEnumerable<SensorDTO> GetSensors();
        DeviceDTO CreateDevice(DeviceDTO device);
        DeviceDTO UpdateDevice(DeviceDTO device);
        DeviceDTO DeleteDevice(Guid deviceId, Guid userId);
    }
}

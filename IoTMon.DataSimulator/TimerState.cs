using IoTMon.Models.DTO;

namespace IoTMon.DataSimulator
{
    public class TimerState
    {
        public DeviceDTO Device { get; set; }
        public SensorDTO Sensor { get; set; }
        public TimerState(DeviceDTO device, SensorDTO sensor)
        {
            this.Device = device;
            this.Sensor = sensor;
        }
    }
}

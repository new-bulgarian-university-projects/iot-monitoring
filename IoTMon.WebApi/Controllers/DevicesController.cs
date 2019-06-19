using IoTMon.DataServices.Contracts;
using IoTMon.Models.AMQP;
using IoTMon.Models.DTO;
using IoTMon.Models.TimeSeries;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IoTMon.WebApi.Controllers
{
    [Route("api/devices")]
    [ApiController]
    public class DevicesController : ControllerBase
    {
        private readonly ITimeSeriesProvider influxDb;
        private readonly IDeviceService deviceService;

        public DevicesController(ITimeSeriesProvider influxDb, IDeviceService deviceService)
        {
            this.influxDb = influxDb ?? throw new ArgumentNullException(nameof(influxDb));
            this.deviceService = deviceService ?? throw new ArgumentNullException(nameof(deviceService));
        }

        [HttpGet()]
        public ActionResult GetAllDevices()
        {
            var devices = this.deviceService.GetDevices();
            return Ok(devices);
        }

        [HttpGet("{deviceId:guid}/sensors/{sensor}")]
        public async Task<ActionResult<IEnumerable<Message>>> GetDeviceSensor(Guid deviceId, string sensor,
            [FromQuery(Name = "from")] DateTime? from,
            [FromQuery(Name = "to")] DateTime? to)
        {
            var result = await influxDb.QueryAsync(deviceId, sensor, from, to);
            var processed = result.Select(r => new ChartData(r.Time, Convert.ToDouble(r.Value))).ToList();
            return Ok(processed);
        }

        [HttpPost()]
        public ActionResult CreateDevice(DeviceDTO device)
        {
            try
            {
                var result = this.deviceService.CreateDevice(device);
                return this.Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "Server error");
            }
        }

        [HttpDelete("{deviceId:guid}")]
        public ActionResult DeleteDevice(Guid deviceId)
        {
            try
            {
                var deleted = this.deviceService.DeleteDevice(deviceId);
                if (deleted == null)
                {
                    return this.BadRequest("Could not find device with id " + deviceId);
                }
                return this.Ok(deleted);
            }
            catch (Exception)
            {
                return this.StatusCode(500, "Server Error");
            }
        }

        [HttpGet("~/api/sensors")]
        public ActionResult GetSensors()
        {
            var result = this.deviceService.GetSensors();
            return this.Ok(result);
        }
    }
}
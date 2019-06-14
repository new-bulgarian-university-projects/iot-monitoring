using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IoTMon.DataServices.Contracts;
using IoTMon.Models.AMQP;
using IoTMon.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<ActionResult<IEnumerable<Message>>> GetDeviceSensor(Guid deviceId, string sensor)
        {
            var result = await influxDb.QueryAsync(deviceId, sensor);
            var processed = result.Select(r => new { Date = r.Time, Value = r.Value }).ToList();
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

        [HttpGet("~/api/sensors")]
        public ActionResult GetSensors()
        {
            var result = this.deviceService.GetSensors();
            return this.Ok(result);
        }
    }
}
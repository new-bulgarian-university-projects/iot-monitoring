using IoTMon.DataServices;
using IoTMon.DataServices.Contracts;
using IoTMon.Models.SignalR;
using IoTMon.Models.TimeSeries;
using IoTMon.Services;
using IoTMon.Services.Contracts;
using IoTMon.WebApi.HubConfig;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IoTMon.WebApi.Controllers
{
    [Route("api/chart")]
    [ApiController]
    public class ChartController : ControllerBase
    {
        private IHubContext<ChartHub> hub;
        private ITimeSeriesProvider influxDbClient;
        public List<TimerManager> timers = new List<TimerManager>();

        public ChartController(IHubContext<ChartHub> hub, ITimeSeriesProvider influxDbClient)
        {
            this.hub = hub;
            this.influxDbClient = influxDbClient;
        }


        [HttpGet()]
        public IActionResult Get([FromQuery(Name = "deviceId")] Guid deviceId,
            [FromQuery(Name = "sensor")] string sensor,
            [FromQuery(Name = "connId")] string connId)
        {
            if (deviceId == Guid.Empty)
            {
                return this.BadRequest("DeviceId is missing");
            }
            if (string.IsNullOrWhiteSpace(sensor))
            {
                return this.BadRequest("Sensor is missing");
            }
            if (string.IsNullOrWhiteSpace(connId))
            {
                return this.BadRequest("Connection ID is missing");
            }

            var filter = new SignalRFilter()
            {
                DeviceId = deviceId,
                Sensor = sensor,
                From = (DateTime.Now - TimeSpan.FromSeconds(2))
            };
            var dManager = new DataManager(this.influxDbClient);
            // refactor using a Timer Factory
            var timerManager = new TimerManager(async () =>
            {
                var collection = await dManager.Get(filter);
                await hub.Clients.All.SendAsync("transferchartdata", collection);
            });
            Utils.timers[connId] = timerManager;

            return Ok(new { Message = "Request Completed" });
        }


        [HttpGet("timers")]
        public ActionResult GetTimers()
        {
            return Ok(Utils.timers);
        }

        // TODO: when stopping connection hit an endpodin to clear the timers

    }
}

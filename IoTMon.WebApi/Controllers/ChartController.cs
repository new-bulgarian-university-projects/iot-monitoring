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
        private readonly IHubContext<ChartHub> hub;
        private readonly ITimeSeriesProvider influxDbClient;
        private readonly IDataManager dataManager;
        private readonly IDeviceService deviceService;


        public ChartController(IHubContext<ChartHub> hub, ITimeSeriesProvider influxDbClient,
            IDataManager dataManager, IDeviceService deviceService)
        {
            this.hub = hub ?? throw new ArgumentNullException(nameof(hub));
            this.influxDbClient = influxDbClient ?? throw new ArgumentNullException(nameof(influxDbClient));
            this.dataManager = dataManager ?? throw new ArgumentNullException(nameof(dataManager));
            this.deviceService = deviceService ?? throw new ArgumentNullException(nameof(deviceService));
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
                From = DateTime.Now
            };

            var device = this.deviceService.GetDeviceById(deviceId);
            //var dt = DateTime.Now;
            // refactor using a Timer Factory
            Func<DateTime, Task<DateTime?>> func = async (dt) =>
                {
                    filter.From = dt;
                    var collection = await dataManager.Get(filter);
                    await hub.Clients.All.SendAsync("transferchartdata", collection);
                    return collection.LastOrDefault()?.Date;
                };
            var timerManager = new TimerManager(device.IntervalInSeconds, func);

            Utils.timers[connId] = timerManager;

            return Ok(new { Message = "Request Completed" });
        }


        [HttpGet("timers")]
        public ActionResult GetTimers()
        {
            return Ok(Utils.timers);
        }
    }
}

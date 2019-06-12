using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IoTMon.DataServices.Contracts;
using IoTMon.Models.AMQP;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IoTMon.WebApi.Controllers
{
    [Route("api/devices")]
    [ApiController]
    public class DevicesController : ControllerBase
    {
        private readonly ITimeSeriesProvider influxDb;

        public DevicesController(ITimeSeriesProvider influxDb)
        {
            this.influxDb = influxDb ?? throw new ArgumentNullException(nameof(influxDb));
        }

        [HttpGet("{deviceId:guid}/sensors/{sensor}")]
        public async Task<ActionResult<IEnumerable<Message>>> Get(Guid deviceId, string sensor)
        {
            var result = await influxDb.QueryAsync(deviceId, sensor);
            var processed = result.Select(r => new { Date = r.Time, Value = r.Value }).ToList();
            return Ok(processed);
        }
    }
}
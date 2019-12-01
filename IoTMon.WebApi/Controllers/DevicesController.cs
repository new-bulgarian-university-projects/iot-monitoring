using IoTMon.DataServices.Contracts;
using IoTMon.Models.AMQP;
using IoTMon.Models.DTO;
using IoTMon.Models.Enums;
using IoTMon.Models.TimeSeries;
using IoTMon.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IoTMon.WebApi.Controllers
{
    [Route("api/devices")]
    [ApiController]
    public class DevicesController : ControllerBase
    {
        private readonly ITimeSeriesProvider influxDb;
        private readonly IDeviceService deviceService;
        private readonly IAlertService alertService;
        private readonly IDataParser dataParser;

        public DevicesController(ITimeSeriesProvider influxDb, IDeviceService deviceService,
            IAlertService alertService, IDataParser dataParser)
        {
            this.influxDb = influxDb ?? throw new ArgumentNullException(nameof(influxDb));
            this.alertService = alertService ?? throw new ArgumentNullException(nameof(alertService));
            this.deviceService = deviceService ?? throw new ArgumentNullException(nameof(deviceService));
            this.dataParser = dataParser ?? throw new ArgumentNullException(nameof(dataParser));
        }

        [HttpGet()]
        public ActionResult GetPublicDevices()
        {
            try
            {
                var devices = this.deviceService.GetDevices(scope: ScopeEnum.Public);
                return Ok(devices);
            }
            catch (Exception)
            {
                return this.StatusCode(500, "Server Error on getting public devices");
            }
        }

        [HttpGet("{deviceId:guid}/sensors/{sensor}")]
        public async Task<ActionResult<IEnumerable<Message>>> GetDeviceSensor(Guid deviceId, string sensor,
            [FromQuery(Name = "from")] DateTime? from,
            [FromQuery(Name = "to")] DateTime? to)
        {
            var result = await influxDb.QueryAsync(deviceId, sensor, from, to);

            var processed = this.dataParser.ParseMessages(sensor, result);
            return Ok(processed);
        }

        [HttpGet("{deviceId:guid}")]
        public ActionResult GetDeviceById(Guid deviceId)
        {
            try
            {

                var device = this.deviceService.GetDeviceById(deviceId);
                var userId = GetClaim(this.User, "id");
                if (!device.IsPublic && device.UserId.ToString() != userId)
                {
                    return Unauthorized("This private device is not your belonging !");
                }
                return Ok(device);
            }
            catch (Exception)
            {
                return this.StatusCode(500, "Server error while getting device with ID " + deviceId);
            }

        }

        [HttpGet("{deviceId:guid}/sensors/{sensorName}/alerts-history")]
        public async Task<ActionResult> GetAlertHistory(Guid deviceId, string sensorName)
        {
            try
            {
                var device = this.deviceService.GetDeviceById(deviceId);
                var userId = GetClaim(this.User, "id");
                if (!device.IsPublic && device.UserId.ToString() != userId)
                {
                    return Unauthorized("This private device is not your belonging !");
                }
                var alerts = await this.alertService.GetAlerts(deviceId, sensorName);
                return this.Ok(alerts);
            }
            catch (Exception)
            {
                return this.StatusCode(500, "Server Error on getting Alerts History for device with ID " + deviceId);
            }
        }

        [Authorize]
        [HttpPut("{deviceId:guid}")]
        public ActionResult UpdateDevice(DeviceDTO device)
        {
            try
            {
                var userId = GetClaim(this.User, "id");
                if (string.IsNullOrWhiteSpace(userId))
                {
                    return this.BadRequest("User Id is not presented in JWT.");
                }
                if (userId != device.UserId.ToString())
                {
                    return this.Unauthorized("Device is not your belonging !");
                }
                var updated = this.deviceService.UpdateDevice(device);
                return this.Ok(updated);
            }
            catch (Exception)
            {
                return this.StatusCode(500, "Server Error on updating device with ID " + device.Id);
            }
        }

        [Authorize]
        [HttpPost()]
        public ActionResult CreateDevice(DeviceDTO device)
        {
            try
            {
                var userId = GetClaim(this.User, "id");
                if (string.IsNullOrWhiteSpace(userId))
                {
                    return this.BadRequest("user id is not present in JWT");
                }
                var result = this.deviceService.CreateDevice(device, new Guid(userId));
                return this.Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "Server error");
            }
        }

        private static string GetClaim(ClaimsPrincipal user, string claimName)
        {
            var claimsIdentity = user.Identity as ClaimsIdentity;
            var idClaim = claimsIdentity?.FindFirst(claimName);
            if (idClaim != null)
            {
                return idClaim.Value;
            }
            return null;
        }



        [Authorize]
        [HttpDelete("{deviceId:guid}")]
        public ActionResult DeleteDevice(Guid deviceId)
        {
            try
            {
                var userId = GetClaim(this.User, "id");
                if (string.IsNullOrWhiteSpace(userId))
                {
                    return this.BadRequest("User Id is not presented in JWT.");
                }
                var deleted = this.deviceService.DeleteDevice(deviceId, new Guid(userId));
                if (deleted == null)
                {
                    return this.BadRequest("Could not find YOUR device with id " + deviceId);
                }
                return this.Ok(deleted);
            }
            catch (Exception)
            {
                return this.StatusCode(500, "Server Error");
            }
        }

        [HttpGet("~/api/users/{userId:guid}/devices")]
        public IActionResult GetUserDevices(Guid userId)
        {
            try
            {
                var scope = ScopeEnum.Public;
                string uId = GetClaim(this.User, "id");

                if (uId != null && uId == userId.ToString())
                {
                    scope = ScopeEnum.All;
                }

                var devices = this.deviceService.GetDevices(scope: scope, userId: userId);
                return Ok(devices);

            }
            catch (Exception)
            {
                return this.StatusCode(500, "Server Error on getting devices for user w/ ID " + userId);
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
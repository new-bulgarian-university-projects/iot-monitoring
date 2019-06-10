using IoTMon.Data;
using IoTMon.DataServices.Contracts;
using IoTMon.Models;
using IoTMon.Models.DTO;
using IoTMon.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace IoTMon.DataServices
{
    public class DeviceService : IDeviceService
    {
        private readonly ApplicationDbContext dbContext;

        public DeviceService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException("dbContext");
        }

        public IEnumerable<DeviceDTO> GetDevices(StatusEnum status = StatusEnum.All, ScopeEnum scope = ScopeEnum.All)
        {
            IQueryable<Device> query = this.dbContext.Devices;

            if (status == StatusEnum.Deactivated)
            {
                query = query.Where(d => d.IsActivated == false);
            }
            else if(status == StatusEnum.Activated)
            {
                query = query.Where(d => d.IsActivated == true);
            }


            if (scope == ScopeEnum.Private)
            {
                query = query.Where(d => d.IsPublic == false);
            }
            else if(scope == ScopeEnum.Public)
            {
                query = query.Where(d => d.IsPublic == true);
            }

            var devices = query.Include(d => d.DeviceSensors)
                .ThenInclude(r => r.Sensor)
                .Select(d => new DeviceDTO(d)).ToList();

            return devices;
        }
    }
}

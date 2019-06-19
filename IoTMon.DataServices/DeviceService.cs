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

        public IEnumerable<SensorDTO> GetSensors()
        {
            var sensors = this.dbContext.Sensors.Select(s => new SensorDTO(s));
            return sensors;
        }

        public IEnumerable<DeviceDTO> GetDevices(StatusEnum status = StatusEnum.All, ScopeEnum scope = ScopeEnum.All)
        {
            IQueryable<Device> query = this.dbContext.Devices
                .Where(d => d.IsDeleted == false);

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

        public DeviceDTO CreateDevice(DeviceDTO device)
        {
            var newDevice = new Device()
            {
                Id = Guid.NewGuid(),
                DeviceName = device.DeviceName,
                IsPublic = device.IsPublic,
                IsActivated = device.IsActivated,
                IsDeleted = false,
                IntervalInSeconds = device.IntervalInSeconds
            };

            foreach (var sensorId in device.SensorIds)
            {
                newDevice.DeviceSensors.Add(new DeviceSensor()
                {
                    DeviceId = newDevice.Id,
                    SensorId = new Guid(sensorId)
                });
            }

            this.dbContext.Devices.Add(newDevice);
            this.dbContext.SaveChanges();

            return new DeviceDTO(newDevice);
        }

        public DeviceDTO DeleteDevice(Guid deviceId)
        {
            var device = this.dbContext.Devices.SingleOrDefault(d => d.Id == deviceId);
            if(device == null)
            {
                return null;
            }

            device.IsDeleted = true;
            this.dbContext.Devices.Update(device);
            this.dbContext.SaveChanges();

            return new DeviceDTO(device);
        }

        public DeviceDTO GetDeviceById(Guid deviceId)
        {
            var device = this.dbContext.Devices
                        .Include(d => d.DeviceSensors)
                        .ThenInclude(ds => ds.Sensor)
                        .Single(d => d.Id == deviceId);

            return new DeviceDTO(device);
        
        }

        public DeviceDTO UpdateDevice(DeviceDTO device)
        {
            if (device == null)
            {
                throw new ArgumentNullException(nameof(device));
            }

            var target = this.dbContext.Devices.Single(d => d.Id == device.Id);
            if(target != null)
            {
                target.IntervalInSeconds = device.IntervalInSeconds;
                target.IsPublic = device.IsPublic;
                target.IsActivated = device.IsActivated;

                this.dbContext.Devices.Update(target);
                this.dbContext.SaveChanges();
            }
            return new DeviceDTO(target);
        }
    }
}

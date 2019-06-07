using IoTMon.Data;
using IoTMon.DataServices.Contracts;
using System;
using System.Collections.Generic;
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
    }
}

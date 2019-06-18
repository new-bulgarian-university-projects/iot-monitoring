using IoTMon.Services;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IoTMon.WebApi.HubConfig
{
    public class ChartHub : Hub
    {
        public string GetConnectionId()
        {
            return this.Context.ConnectionId;
        }
        
        public override Task OnDisconnectedAsync(Exception exception)
        {
            string connId = this.Context.ConnectionId;
            if (Utils.timers.ContainsKey(connId))
            {
                Utils.timers[connId].Dispose();
                Utils.timers.Remove(connId);
            }

            return base.OnDisconnectedAsync(exception);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace IoTMon.Services.Contracts
{
    public interface IAlertNotifier
    {
        Task<HttpStatusCode> Execute(object properties);
    }
}

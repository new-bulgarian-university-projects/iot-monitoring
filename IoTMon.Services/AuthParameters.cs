using System;
using System.Collections.Generic;
using System.Text;

namespace IoTMon.Services
{
    public class AuthParameters
    {
        public string Secret { get; set; }
        public string Issuer { get; set; }
        public int LifeTimeMinutes { get; set; }
    }
}

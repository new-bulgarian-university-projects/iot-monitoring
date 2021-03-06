﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace IoTMon.Services
{
    public static class Utils
    {
        public static Dictionary<string,TimerManager> timers = new Dictionary<string, TimerManager>();

        public static byte[] Serialize(object data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            string jsonified = JsonConvert.SerializeObject(data);
            byte[] buffer = Encoding.UTF8.GetBytes(jsonified);
            return buffer;
        }

        public static T Deserialize<T>(byte[] data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            string jsonified = Encoding.UTF8.GetString(data);
            T deserialized = JsonConvert.DeserializeObject<T>(jsonified);
            return deserialized;
        }

        public static IConfiguration Configure(string relativePath = @"../../../../IoTMon.WebApi")
        {
            var absolutePath = Path.GetFullPath(relativePath);
            var fileProvider = new PhysicalFileProvider(absolutePath);

            var configuration = new ConfigurationBuilder()
                  .AddJsonFile(fileProvider, "appsettings.json", false, true)
                  .Build();

            return configuration;
        }
    }
}

using Newtonsoft.Json;
using SmartShop.Services.Interfaces;
using System;
using System.IO;
using System.Text;
using SmartShop.Data.Configuration;

namespace SmartShop.Services.Services
{
    internal class DataService : IDataService
    {
        public ConfigJson GetConnectionString()
        {
            var configFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Configuration", "config.json");
            using var fs = File.OpenRead(configFile);
            using var sr = new StreamReader(fs, new UTF8Encoding(false));
            var json = sr.ReadToEnd();

            var configJson = JsonConvert.DeserializeObject<ConfigJson>(json);

            return configJson;
        }
    }
}

using Newtonsoft.Json;
using SmartShop.Data.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartShop.Data.Helpers
{
    internal class DataHelper
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

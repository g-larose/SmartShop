using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartShop.Data.Configuration
{
    public class ConfigJson
    {
        [JsonProperty("ConnectionString")]
        public string ConnectionString { get; set; }
    }
}

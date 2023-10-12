using Smart_Shop.Interfaces;
using Smart_Shop.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Smart_Shop.Services
{
    public class DataService : IDataService
    {
        public string GetConnectionString()
        {
            var jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Settings", "appsettings.json");
            var json = File.ReadAllText(jsonPath);
            var conString = JsonSerializer.Deserialize<ConfigJson>(json);

            return conString!.ConnectionString!;
        }
    }
}

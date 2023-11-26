using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Smart_Shop.Data;
using Smart_Shop.Interfaces;
using Smart_Shop.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Smart_Shop.Factories
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {

        public AppDbContextFactory()
        {
           
        }

        public AppDbContext CreateDbContext(string[] args = null)
        {
            var json = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Settings", "appsettings.json"));
            var conStr = JsonSerializer.Deserialize<ConfigJson>(json);
            var options = new DbContextOptionsBuilder<AppDbContext>();
            options.UseNpgsql(conStr!.ConnectionString);

            return new AppDbContext(options.Options);
            
        }

    }
}

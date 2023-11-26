using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Smart_Shop.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Smart_Shop.Data
{
    public class AppDbContext : DbContext
    {
       
        public DbSet<Customer> Customers { get; set; }
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            var json = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Settings", "appsettings.json"));
            var conStr = JsonSerializer.Deserialize<ConfigJson>(json);
            optionsBuilder.UseNpgsql(conStr!.ConnectionString);
        }
    }
}

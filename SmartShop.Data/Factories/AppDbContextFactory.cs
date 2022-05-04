using Microsoft.EntityFrameworkCore;
using System;

namespace SmartShop.Data.Factories
{
    internal class AppDbContextFactory : IDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext()
        {
            var dataService = new DataService();
            var conString = dataService.GetConfigurationJson();
            var connStr = conString.ConnectionString;
            var options = new DbContextOptionsBuilder<AppDbContext>();

            options.UseNpgsql(connStr);

            return new AppDbContext(options.Options);
        }
    }
}

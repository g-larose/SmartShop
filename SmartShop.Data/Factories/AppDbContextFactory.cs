using Microsoft.EntityFrameworkCore;
using SmartShop.Data.Helpers;
using System;

namespace SmartShop.Data.Factories
{
    internal class AppDbContextFactory : IDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext()
        {
            var dataHelper = new DataHelper();
            var conString = dataHelper.GetConnectionString();
            var connStr = conString.ConnectionString;
            var options = new DbContextOptionsBuilder<AppDbContext>();

            options.UseNpgsql(connStr);

            return new AppDbContext(options.Options);
        }
    }
}

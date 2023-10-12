using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Smart_Shop.Data;
using Smart_Shop.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Shop.Factories
{
    public class DbContextFactory : IDbContextFactory<AppDbContext>
    {
        private readonly IDataService _dataService;
        public DbContextFactory(IDataService dataService)
        {
            _dataService = dataService;
        }

        public AppDbContext CreateDbContext()
        {
            var conStr = _dataService.GetConnectionString();
            var options = new DbContextOptionsBuilder<AppDbContext>();
            options.UseNpgsql(conStr);

            return new AppDbContext(options.Options);
        }
    }
}

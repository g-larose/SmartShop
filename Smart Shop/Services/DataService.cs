using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
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

namespace Smart_Shop.Services
{
    public class DataService : IDataService
    {
        private readonly IDbContextFactory<AppDbContext> _dbFactory;

        public DataService(IDbContextFactory<AppDbContext> dbFactory)
        {
            _dbFactory = dbFactory ?? throw new ArgumentNullException(nameof(dbFactory));
        }

        public string GetConnectionString()
        {
            var jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Settings", "appsettings.json");
            var json = File.ReadAllText(jsonPath);
            var conString = JsonSerializer.Deserialize<ConfigJson>(json);

            return conString!.ConnectionString!;
        }

        /// <summary>
        /// Generate a unique 5 digit Invoice number. format 00000
        /// </summary>
        /// <param name="size"></param>
        /// <returns>string</returns>
        public async Task<string> GenerateInvoiceNumber(int size)
        {
            using var db = _dbFactory.CreateDbContext();
            var invNum = await db.Customers
                            .Include(x => x.Invoices)
                            .LastAsync();
            var newInvNum = invNum.Invoices?.Select(x => x.InvoiceNumber) + 1.ToString();
            return newInvNum;
        }
    }  
}

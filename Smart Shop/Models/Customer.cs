using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Shop.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public Guid Identifier { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public string ContactName { get; set; } = string.Empty;
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Phone]
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Zipcode { get; set; } = string.Empty;
        public int InvoiceId { get; set; }
        public virtual ICollection<Invoice>? Invoices { get; set; }
    }
}

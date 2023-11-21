namespace Smart_Shop.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public int InvoiceNumber { get; set; }
        public string? RefrenceNumber { get; set; }
        public string? PO { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public double Total { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime ArrivedDate { get; set; }
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }
        public virtual ICollection<Item>? Items { get; set; }

    }
}
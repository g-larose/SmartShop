namespace Smart_Shop.Models
{
    public class Item
    {
        public int Id { get; set; }
        public Guid Identifier { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public double Price { get; set; }

    }
}
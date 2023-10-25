namespace WebAPI_LKP.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public double Price { get; set; }
        public string Description { get; set; } = string.Empty;
        public bool IsAvailable { get; set; } = true;
        public uint Count { get; set; } = 0;
        public string Image { get; set; } = string.Empty;
        public Guid OrderId { get; set; }
        public Order Order { get; set; } = null!;
    }
}

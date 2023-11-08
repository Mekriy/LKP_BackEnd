namespace WebAPI_LKP.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public double TotalPrice { get; set; } = 0;
        public bool Delivery { get; set; }
        public Guid UserId { get; set; }
        public List<Product> Products { get; set; } = null!;
    }
}

using WebAPI_LKP.Models;

namespace WebAPI_LKP.DTO
{
    public class OrderDTO
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        public double TotalPrice { get; set; }
        public bool Delivery { get; set; }
        public List<Product> Products { get; set; }
    }
}

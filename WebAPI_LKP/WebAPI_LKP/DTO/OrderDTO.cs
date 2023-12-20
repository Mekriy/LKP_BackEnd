using WebAPI_LKP.Models;
using WebAPI_LKP.Models.Enums;

namespace WebAPI_LKP.DTO
{
    public class OrderDTO
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public Delivery Delivery { get; set; }
        public int Quantity { get; set; }
    }
}

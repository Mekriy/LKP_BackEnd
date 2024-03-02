using System.ComponentModel.DataAnnotations.Schema;
using WebAPI_LKP.Models.Enums;

namespace WebAPI_LKP.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public Delivery Delivery { get; set; }
        public int Quantity { get; set; }
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
        public DateTime CreatedDate { get; set; }

        [NotMapped]
        public double TotalPrice { get; private set; }
    }
}

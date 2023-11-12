using System.ComponentModel.DataAnnotations;

namespace WebAPI_LKP.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        [MaxLength(50)]
        public string ProductName { get; set; } = string.Empty;
        public double Price { get; set; }
        [MaxLength(100)]
        public string Description { get; set; } = string.Empty;
        public bool IsAvailable { get; set; } = true;
        [MaxLength(100)]
        public string Image { get; set; } = string.Empty;
        public Guid? OrderId { get; set; } 
        public Order? Order { get; set; }
    }
}

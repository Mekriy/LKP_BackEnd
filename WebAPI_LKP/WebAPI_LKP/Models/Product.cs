using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI_LKP.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        [MaxLength(50)]
        public string ProductName { get; set; }
        public double Price { get; set; }
        [MaxLength(255)]
        public string Description { get; set; }
        [MaxLength(255)]
        public string Image { get; set; }
        public string Type { get; set; }
    }
}

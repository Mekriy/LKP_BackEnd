using System.ComponentModel.DataAnnotations;

namespace WebAPI_LKP.DTO
{
    public class ProductDTO
    {
        public Guid Id { get; set; }
        public string? ProductName { get; set; }
        public double Price { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace WebAPI_LKP.Models
{
    public class User
    {
        public Guid Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(70)]
        public string Email { get; set; } = string.Empty;
        [MaxLength(70)]
        public string Password { get; set; } = string.Empty;
        public bool IsAdmin { get; set; }
        public List<Order> Orders { get; set; } = null!;
    }
}

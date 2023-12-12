using System.ComponentModel.DataAnnotations;

namespace WebAPI_LKP.DTO
{
    public class TokenRequest
    {
        [Required]
        public string? Token { get; set; }
        [Required]
        public string? RefreshToken { get; set; }
    }
}

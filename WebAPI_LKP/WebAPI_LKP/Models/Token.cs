using System.ComponentModel.DataAnnotations;

namespace WebAPI_LKP.Models
{
    public class Token
    {
        [Key]
        public string JwtToken { get; set; } = string.Empty;
    }
}

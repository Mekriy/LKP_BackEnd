using System.ComponentModel.DataAnnotations;

namespace WebAPI_LKP.Models.Tokens
{
    public class Token
    {
        [Key]
        public string JwtToken { get; set; } = string.Empty;
    }
}

namespace WebAPI_LKP.DTO
{
    public class UserDTO
    {
        public string? Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? ConfirmPassword { get; set; }
        public bool? IsAdmin { get; set; }
    }
}

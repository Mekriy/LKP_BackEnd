using WebAPI_LKP.Models;

namespace WebAPI_LKP.Services.Authentication.Verifications
{
    public interface IEmailService
    {
        Task<bool> SendEmail(User user);
        Task<string> CallbackUrl(User user, string code);
        Task<bool> VerifyEmail(User user, string code);
    }
}

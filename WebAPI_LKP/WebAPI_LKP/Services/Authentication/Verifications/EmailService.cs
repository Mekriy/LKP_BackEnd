using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Security.Policy;
using WebAPI_LKP.Models;
using MimeKit;
using MimeKit.Text;
using MailKit.Net.Smtp;
using MailKit.Security;

namespace WebAPI_LKP.Services.Authentication.Verifications
{
    public class EmailService : IEmailService
    {
        private readonly UserManager<User> _userManager;
        //private readonly IHttpContextAccessor _contextAccessor; -- for Request.Scheme and Request.Host

        public EmailService(
            UserManager<User> userManager
            /* IHttpContextAccessor contextAccessor */)
        {
            _userManager = userManager;
            //_contextAccessor = contextAccessor;
        }

        public async Task<bool> SendEmail(User user)
        {
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var callback_url = await CallbackUrl(user, code);

            try
            {
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse("lkp.pizza.zyila.koshenya@gmail.com"));
                email.To.Add(MailboxAddress.Parse(user.Email));
                email.Subject = "Email verification";
                email.Body = email.Body = new TextPart(TextFormat.Html)
                {
                    Text = $@"<!DOCTYPE html>
<html>
<head>
  <meta http-equiv=""Content-Type"" content=""text/html"" charset=""UTF-8"" />
  <title>Email Verification</title>
  <link href=""https://fonts.googleapis.com/css2?family=Lato&display=swap"" rel=""stylesheet"">
  <style>
    .button-container {{
      text-align: center;
    }}
    .verify-button {{
      background-color: transparent; /* Transparent button background */
      color: black; /* Text color */
      width: 175px;
      height: 35px;
      font-family: 'Lato', sans-serif;
      font-size: 1em;
      border: 2px solid black; /* Black border */
      border-radius: 20px;
      text-decoration: none;
      display: inline-block;
      line-height: 35px;
      text-align: center;
    }}
    .email-text {{
      color: #333;
      font-size: 1em;
    }}
  </style>
</head>
<body>

  <table cellpadding=""0"" cellspacing=""0"" border=""0"">
    <tr>
      <td>
        <h2>Email Verification</h2>
        <p class=""email-text"">Thank you for signing up! To verify your email address, please click the link below:</p>
      </td>
    </tr>
    <tr>
      <td style=""text-align: center;"">
        <div class=""button-container"">
          <a class=""verify-button"" href=""{callback_url}"">Verify Email</a>
        </div>
      </td>
    </tr>
    <tr>
      <td>
        <p class=""email-text"">If you didn't sign up for this service, please ignore this email.</p>
        <p class=""email-text"" style=""margin-bottom: 5px;"">Best regards,</p>
        <p class=""email-text"" style=""margin: 0;"">Little Kittens Pizzeria</p>
      </td>
    </tr>
  </table>
</body>
</html>"
                };

                using var smtp = new SmtpClient();
                await smtp.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync("lkp.pizza.zyila.koshenya@gmail.com", "rumm bgws dptz vpsb");

                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);
                
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<string> CallbackUrl(User user, string code)
        {
            //var requestScheme = _contextAccessor.HttpContext.Request.Scheme; // -- gives https
            //var requestHost = _contextAccessor.HttpContext.Request.Host; // -- gives localhost44313
            
            var ngrok = ConstantVariables.ngrok; // -- Constant variable of ngrok link, always re-generates
            var callback_url = ngrok + "/api/User/ConfirmEmail" + $"?userId={user.Id}&code={code}";

            return callback_url;
        }
        public async Task<bool> VerifyEmail(User user, string code)
        {
            var result = await _userManager.ConfirmEmailAsync(user, code);

            return result.Succeeded ? true : false;
        }
    }
}

using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using WebAPI_LKP.DTO;
using WebAPI_LKP.Interfaces.Services;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using WebAPI_LKP.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using WebAPI_LKP.Services.Authentication.Verifications;
using System.Text;

namespace WebAPI_LKP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowMyOrigins")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;
        public UserController(
            IUserService userService,
            IEmailService emailService)
        {
            _userService = userService;
            _emailService = emailService;
        }
        [HttpOptions]
        public IActionResult PreflightRoute()
        {
            return NoContent();
        }
        [HttpGet("GetUser")]
        public async Task<IActionResult> GetUser([FromQuery] string userEmail)
        {
            var user = await _userService.GetUserDTO(userEmail);
            if (user == null)
            {
                return NotFound("User doesn't exist!");
            }
            return Ok(user);
        }
        [HttpGet("GetUsers")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userService.GetAllUsersDTO();
            if (users == null || users.Count == 0)
                return BadRequest("There are no tasks for this user!");
            else
                return Ok(users);
        }
        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDTO userLogin)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _userService.UserExist(userLogin.Email))
                return NotFound("User doesn't exist!");

            var user = await _userService.GetUser(userLogin.Email);

            if (!user.EmailConfirmed)
                return BadRequest("Email needs to be confirmed"); 

            if (!await _userService.CheckPassword(user, userLogin.Password))
                return BadRequest("Password doesn't match!");

            var token = _userService.GenerateJwtToken(user);

            return Ok(token);
        }
        [AllowAnonymous]
        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp([FromBody] UserSignUpDTO userSignUp)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (await _userService.UserExist(userSignUp.Email))
                return BadRequest("User already exists!");

            if (await _userService.NameOrEmailCheck(userSignUp.Name, userSignUp.Email))
                return BadRequest("Name or Email is already used!");

            if (await _userService.CreateUser(userSignUp))
            {
                var createdUser = await _userService.GetUser(userSignUp.Email);
                //var token = _userService.GenerateJwtToken(userForToken);
                //if(token == null)
                //    return StatusCode(500, "Something went wrong while creating token!");
                //else
                //    return Ok(token);
                if (await _emailService.SendEmail(createdUser))
                    return Ok("email has been sent!");
                else
                    return StatusCode(500, "Something went wrong while sending email");
            }
            else
                return StatusCode(500, "Something went wrong while saving on server!");
        }
        [AllowAnonymous]
        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
                return BadRequest("Invalid email confirmation url");

            var user = await _userService.GetUserByGuid(userId);

            if (user == null)
                return BadRequest("Invalid email parameter");

            code = code.Replace(' ', '+');

            if (await _emailService.VerifyEmail(user, code))
                return Content(VerificationHtmls.htmlSuccessVerification, "text/html");
            else
                return Content(VerificationHtmls.htmlFailVerification, "text/html");
        }
    }
}

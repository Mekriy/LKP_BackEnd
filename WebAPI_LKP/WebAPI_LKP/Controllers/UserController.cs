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
using WebAPI_LKP.Services.Authentication;

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
                return BadRequest(new AuthResult()
                {
                    Errors = new List<string>()
                    {
                        "Invalid parameters",
                        ModelState.ToString()
                    },
                    Result = false
                });

            if (!await _userService.UserExist(userLogin.Email))
                return NotFound(new AuthResult()
                {
                    Errors = new List<string>()
                    {
                        "User doesn't exist!"
                    },
                    Result = false
                });

            var user = await _userService.GetUser(userLogin.Email);

            if (!user.EmailConfirmed)
                return BadRequest(new AuthResult()
                {
                    Errors = new List<string>()
                    {
                        "Email needs to be confirmed"
                    },
                    Result = false
                }); 

            if (!await _userService.CheckPassword(user, userLogin.Password))
                return BadRequest(new AuthResult()
                {
                    Errors = new List<string>()
                    {
                        "Password doesn't match!"
                    },
                    Result = false
                });

            var token = await _userService.GenerateJwtToken(user);

            return Ok(token);
        }
        [AllowAnonymous]
        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp([FromBody] UserSignUpDTO userSignUp)
        {
            if (!ModelState.IsValid)
                return BadRequest(new AuthResult()
                {
                    Errors = new List<string>()
                    {
                        "Invalid parameters",
                        ModelState.ToString()
                    },
                    Result = false
                });

            if (await _userService.UserExist(userSignUp.Email))
                return BadRequest(new AuthResult()
                {
                    Errors = new List<string>()
                    {
                        "User already exists!"
                    },
                    Result = false
                });

            if (await _userService.NameOrEmailCheck(userSignUp.Name, userSignUp.Email))
                return BadRequest(new AuthResult()
                {
                    Errors = new List<string>()
                    {
                        "Name or Email is already used!"
                    },
                    Result = false
                });

            if (await _userService.CreateUser(userSignUp))
            {
                var createdUser = await _userService.GetUser(userSignUp.Email);
                //var token = _userService.GenerateJwtToken(userForToken);
                //if(token == null)
                //    return StatusCode(500, "Something went wrong while creating token!");
                //else
                //    return Ok(token);
                if (await _emailService.SendEmail(createdUser))
                    return Ok("Email verification has been sent!");
                else
                    return StatusCode(500, new AuthResult()
                    {
                        Errors = new List<string>()
                        {
                            "Something went wrong while sending email!"
                        },
                        Result = false
                    });
            }
            else
                return StatusCode(500, new AuthResult()
                {
                    Errors = new List<string>()
                        {
                            "Something went wrong while saving on server!"
                        },
                    Result = false
                });
        }
        [AllowAnonymous]
        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
                return BadRequest(new AuthResult() 
                { 
                    Errors = new List<string>() 
                    { 
                        "Invalid email confirmation url" 
                    }, 
                    Result = false 
                });

            var user = await _userService.GetUserByGuid(userId);

            if (user == null)
                return BadRequest(new AuthResult() 
                { 
                    Errors = new List<string>() 
                    { 
                        "Invalid email parameter" 
                    }, 
                    Result = false 
                });

            code = code.Replace(' ', '+');

            if (await _emailService.VerifyEmail(user, code))
                return Content(VerificationHtmls.htmlSuccessVerification, "text/html");
            else
                return Content(VerificationHtmls.htmlFailVerification, "text/html");
        }
        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken([FromBody] TokenRequest tokenRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(new AuthResult()
                {
                    Errors = new List<string>()
                    {
                        "Invalid parameters",
                        ModelState.ToString()
                    },
                    Result = false
                });

            var result = await _userService.VerifyAndGenerateToken(tokenRequest);

            if (result == null)
                return BadRequest(new AuthResult()
                {
                    Errors = new List<string>()
                    {
                        "Invalid tokens"
                    },
                    Result = false
                });

            return Ok(result);
        }
    }
}

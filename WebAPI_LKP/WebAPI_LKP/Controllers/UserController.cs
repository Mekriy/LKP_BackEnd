using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WebAPI_LKP.DTO;
using WebAPI_LKP.Interfaces.Services;

namespace WebAPI_LKP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowMyOrigins")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
           _userService = userService;
        }
        [HttpOptions]
        public IActionResult PreflightRoute()
        {
            Response.Headers.Add("Access-Control-Allow-Origin", "http://localhost:8080");
            Response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE");
            Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Authorization");
            return NoContent();
        }
        [HttpGet("GetUser")]
        public async Task<IActionResult> GetUser([FromQuery] string userEmail)
        {
            var user = await _userService.GetUserDTO(userEmail);
            if(user == null)
            {
                return NotFound("User doesn't exist!");
            }
            return Ok(user);
        }

        [HttpGet("GetUsers")]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(await _userService.GetAllUsersDTO());
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDTO userLogin)
        {
            if (!await _userService.UserExist(userLogin.Email, userLogin.Password))
                return NotFound("User doesn't exist!");

            return Ok();
        }
        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp([FromBody] UserSignUpDTO userSignUp)
        {
            if (await _userService.UserExist(userSignUp.Email, userSignUp.Password))
                return NotFound("User already exists!");

            if (await _userService.NameOrEmailCheck(userSignUp.Name, userSignUp.Email))
                return BadRequest("Name or Email is already used!");
            
            userSignUp.Password = _userService.HashPassword(userSignUp.Password);

            if (await _userService.CreateUser(userSignUp))
            {
                return Ok("User successfully created! Now please log in");
            }
            else
                return StatusCode(500, "Something went wrong while saving on server!");
        }
        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser()
        {
            return Ok();
        }
        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DeleteUser()
        {
            return Ok();
        }
    }
}

using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WebAPI_LKP.DTO;
using WebAPI_LKP.Interfaces.Services;

namespace WebAPI_LKP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
           _userService = userService;
        }
        //create service for this controller
        //Use service for safety logic in http endpoints
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
        public async Task<IActionResult> Login([FromBody] UserDTO userLogin)
        {
            if (!await _userService.UserExist(userLogin.Email, userLogin.Password))
                return NotFound("User doesn't exist!");

            return Ok();
        }
        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp([FromBody] UserDTO userSignUp)
        {
            if (await _userService.UserExist(userSignUp.Email, userSignUp.Password))
                return NotFound("User already exists!");

            if (await _userService.NameOrEmailCheck(userSignUp.Name, userSignUp.Email))
                return BadRequest("Name or Email is already used!");

            if (userSignUp.Password != userSignUp.ConfirmPassword)
                return BadRequest("Password and confirmation password don't match!");
            
            userSignUp.Password = _userService.HashPassword(userSignUp.Password);
            
            //create and save user if not tell why
            return Ok();
        }
        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser()
        {   
            return Ok();
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

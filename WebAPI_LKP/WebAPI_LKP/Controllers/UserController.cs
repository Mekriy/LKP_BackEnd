using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI_LKP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        //create service for this controller
        //Use service for safety logic in http endpoints
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("GetUser")]
        public async Task<IActionResult> GetUser([FromQuery] Guid UserId)
        {
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

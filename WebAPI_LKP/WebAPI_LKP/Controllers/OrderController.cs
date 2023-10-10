using Microsoft.AspNetCore.Mvc;

namespace WebAPI_LKP.Controllers
{
    [ApiController]
    [Route("api/[controller")]
    public class OrderController : Controller
    {
        //service for safety use in http endpoints
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("GetOrder")]
        public async Task<IActionResult> GetOrder([FromQuery] Guid orderId)
        {
            return Ok();
        }
        [HttpPost("CreateOrder")]
        public async Task<IActionResult> CreateOrder()
        {
            return Ok();
        }
        [HttpPut("UpdateOrder")]
        public async Task<IActionResult> UpdateOrder()
        {
            return Ok();
        }
        [HttpDelete("DeleteOrder")]
        public async Task<IActionResult> DeleteOrder()
        {
            return Ok();
        }
    }
}

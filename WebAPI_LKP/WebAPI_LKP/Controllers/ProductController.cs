using Microsoft.AspNetCore.Mvc;

namespace WebAPI_LKP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        //Create service for this controller
        //same here, service for safety use in http endpoints
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("GetProduct")]
        public async Task<IActionResult> GetProduct([FromQuery] Guid ProductId)
        {
            return Ok();
        }
        [HttpPost("CreateProduct")]
        public async Task<IActionResult> CreateProduct()
        {
            return Ok();
        }
        [HttpPut("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct()
        {
            return Ok();
        }
        [HttpDelete("DeleteProduct")]
        public async Task<IActionResult> DeleteProduct()
        {
            return Ok();
        }
    }
}

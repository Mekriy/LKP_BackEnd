using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI_LKP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        [HttpGet]
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

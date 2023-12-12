using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI_LKP.Models.Enums;

namespace WebAPI_LKP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI_LKP.Interfaces.Services;
using WebAPI_LKP.Models.Enums;
using WebAPI_LKP.Services.Authentication;

namespace WebAPI_LKP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [AllowAnonymous]
        [HttpGet("getProducts")]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _productService.GetAllProducts();
            if (products == null || products.Count == 0)
                return BadRequest(new AuthResult()
                {
                    Errors = new List<string>()
                    {
                        "Error while fetching list of products from database"
                    },
                    Result = false
                });
            return Ok(products);
        }
        [HttpPost("Create")]
        public async Task<IActionResult> CreateProduct()
        {
            return Ok();
        }
        [HttpPut("Update")]
        public async Task<IActionResult> UpdateProduct()
        {
            return Ok();
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteProduct()
        {
            return Ok();
        }
    }
}

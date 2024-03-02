using MailKit.Search;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI_LKP.DTO;
using WebAPI_LKP.Interfaces.Services;
using WebAPI_LKP.Models;
using WebAPI_LKP.Services.Authentication;
using WebAPI_LKP.Services.RepositoryServices;

namespace WebAPI_LKP.Controllers
{
    [Route("api/admin/[controller]")]
    [ApiController]
    [Authorize(Policy="RequiredAdmin")]
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

        [HttpGet("Deliveries")]
        public async Task<IActionResult> ProductsToDeliver()
        {
            try
            {
                var productsToDeliver = await _productService.GetProductsToDeliver();

                if (productsToDeliver.Count == 0)
                {
                    return NotFound(new
                    {
                        Message = "No products to deliver at the moment."
                    });
                }

                return Ok(productsToDeliver);
            }
            catch (Exception ex)
            { 
                return StatusCode(500, new
                {
                    Message = "Internal server error",
                    Exception = ex.Message
                });
            }
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateProduct([FromBody] List<ProductDTO> products)
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

            if (await _productService.CreateProduct(products))
                return Ok();
            else
                return BadRequest();
        }
        [HttpPut("Update")]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductDTO updateproductDTO)
        {
            if(updateproductDTO == null)
            {
                return NotFound(new AuthResult()
                {
                    Errors = new List<string>()
                    {
                        "Order not found!",
                        ModelState.ToString()
                    },
                    Result = false
                });
            }

            if (await _productService.UpdateProduct(updateproductDTO))
            {
                return Ok();
            }
            else
            {
                return BadRequest(new AuthResult()
                {
                    Errors = new List<string>()
                    {

                        "Failed to update order!",
                        ModelState.ToString()
                    },
                    Result = false
                });
            }
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            if (await _productService.DeleteProduct(id))
            {
                return Ok();
            }
            else
            {
                return NotFound(new AuthResult()
                {
                    Errors = new List<string>()
                    {
                        "Somth went wrong!",
                        ModelState.ToString()
                    },
                    Result = false
                });
            }
        }
    }
}

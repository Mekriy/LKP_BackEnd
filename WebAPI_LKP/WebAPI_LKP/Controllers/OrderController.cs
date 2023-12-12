using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebAPI_LKP.DTO;
using WebAPI_LKP.Interfaces.Services;
using WebAPI_LKP.Services.Authentication;

namespace WebAPI_LKP.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IUserService _userService;

        public OrderController(
            IOrderService orderService,
            IUserService userService)
        {
            _orderService = orderService;
            _userService = userService;
        }
        [HttpGet("UserOrders")]
        public async Task<IActionResult> GetUserOrders()
        {
            var userEmailClaims = HttpContext.User.FindFirstValue(ClaimTypes.Email);
            var user = await _userService.GetUser(userEmailClaims);

            if (user == null)
                return NotFound(new AuthResult()
                {
                    Errors = new List<string>()
                    {
                        "No user found!"
                    },
                    Result = false
                });

            var userOrders = await _orderService.GetUserOrders(user.Id);
            if (userOrders == null || userOrders.Count == 0)
                return NotFound(new AuthResult()
                {
                    Errors = new List<string>()
                    {
                        "There is no orders for this user!"
                    },
                    Result = false
                });
            return Ok(userOrders);
        }
        [HttpPost("Create")]
        public async Task<IActionResult> CreateOrder([FromBody] List<OrderDTO> menuList)
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

            var userEmailClaims = HttpContext.User.FindFirstValue(ClaimTypes.Email);
            var user = await _userService.GetUser(userEmailClaims);

            if(await _orderService.CreateOrder(menuList, user))
                return Ok();
            else
                return BadRequest();
        }
        [HttpPut("Update")]
        public async Task<IActionResult> UpdateOrder()
        {
            return Ok();
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteOrder()
        {
            return Ok();
        }
    }
}

using WebAPI_LKP.DTO;
using WebAPI_LKP.Models;

namespace WebAPI_LKP.Interfaces.Services
{
    public interface IOrderService
    {
        Task<bool> CreateOrder(List<OrderDTO> orderDtoList, User user);
        Task<List<OrderDTO>> GetUserOrders(Guid userId);
    }
}

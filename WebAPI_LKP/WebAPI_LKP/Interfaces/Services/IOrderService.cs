using WebAPI_LKP.DTO;
using WebAPI_LKP.Models;
using WebAPI_LKP.Repositories;

namespace WebAPI_LKP.Interfaces.Services
{
    public interface IOrderService
    {
        Task<bool> CreateOrder(List<OrderDTO> orderDtoList, User user);
        Task<List<OrderDTO>> GetUserOrders(Guid userId);
        Task<OrderDTO> GetOrder(Guid OrderId);
        Task<bool> UpdateOrder(OrderDTO order, User user);
        Task<bool> DeleteOrder(Guid orderId);
    }
}

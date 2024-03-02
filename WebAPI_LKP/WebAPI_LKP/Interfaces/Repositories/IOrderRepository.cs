using WebAPI_LKP.Models;

namespace WebAPI_LKP.Interfaces.Repositories
{
    public interface IOrderRepository
    {
        Task<Order?> GetOrderById(Guid orderId);
        Task<bool> AddOrders(List<Order> orders);
        Task<List<Order>> GetOrdersById(Guid userId);
        Task<bool> UpdateOrder(Order order);
        Task<bool> SaveAsync();
        Task<bool> DeleteOrder(Guid orderId);
    }
}

using WebAPI_LKP.Models;

namespace WebAPI_LKP.Interfaces.Repositories
{
    public interface IOrderRepository
    {
        Task<bool> AddToOrder(Order order, Product product);
        Task<bool> RemoveFromOrder(Order order, Product product);
        Task<bool> ClearOrder(Order order);
        Task<Order?> GetOrderById(Guid orderId);
        Task<bool> SaveAsync();
    }
}

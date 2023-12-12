using WebAPI_LKP.Models;

namespace WebAPI_LKP.Interfaces.Repositories
{
    public interface IOrderRepository
    {
        Task<bool> CreateOrder(User user, Order order, Product product);
        Task<bool> RemoveOrder(User user, Order order);
        Task<bool> ClearUserOrders(User user);
        Task<bool> AddToOrder(Order order, Product product);
        Task<bool> ClearOrder(Order order);
        Task<Order?> GetOrderById(Guid orderId);
        Task<bool> SaveAsync();
    }
}

using WebAPI_LKP.Models;

namespace WebAPI_LKP.Interfaces.Repositories
{
    public interface IOrderRepository
    {
        void AddToOrder(Order order, Product product);
        void RemoveFromOrder(Order order, Product product);
        void ClearOrder(Order order);
        double CalculateTotalPrice(Order order);
        Order? GetOrderById(Guid orderId);
    }
}

using WebAPI_LKP.Models;

namespace WebAPI_LKP.Interfaces
{
    public interface IOrderRepository
    {
        IEnumerable<Order> AllOrders {  get; }
        Order? GetOrderById(Guid orderId);
        double CalculateTotalPrice(Order order);
    }
}

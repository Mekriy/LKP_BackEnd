using WebAPI_LKP.Interfaces;
using WebAPI_LKP.Models;

namespace WebAPI_LKP.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        readonly DbContexts.AppContext context;
        public OrderRepository(DbContexts.AppContext context)
        {
            this.context = context;
        }

        public IEnumerable<Order> AllOrders
        {
            get
            {
                return context.Orders;
            }
        }

        public Order? GetOrderById(Guid orderId) 
        {
            return AllOrders.FirstOrDefault(o => o.Id == orderId);
        }

        public double CalculateTotalPrice(Order order) 
        { 
            double totalPrice = 0;
            foreach (var product in order.Products) 
            {
                totalPrice += product.Price * product.Count;
            }
            return totalPrice;
        }
    }
}

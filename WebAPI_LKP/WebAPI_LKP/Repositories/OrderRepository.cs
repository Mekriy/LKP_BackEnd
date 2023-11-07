using WebAPI_LKP.Interfaces.Repositories;
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

        public void AddToOrder(Order order, Product product)
        {
            if (context.Orders.SingleOrDefault(o => o.Id == order.Id) != null) 
            {
                context.Orders.SingleOrDefault(o => o.Id == order.Id).Products.Add(product);
            }

            context.SaveChanges();
        }

        public void RemoveFromOrder(Order order, Product product)
        {
            if(context.Orders.SingleOrDefault(o => o.Id == order.Id) != null &&
                context.Products.SingleOrDefault(p => p.Id == product.Id && p.OrderId == product.OrderId) != null)
            {
                context.Orders.SingleOrDefault(o => o.Id == order.Id).Products.Remove(
                    context.Products.SingleOrDefault(p => p.Id == product.Id && p.OrderId == product.OrderId));
            }

            context.SaveChanges();
        }

        public void ClearOrder(Order order)
        {
            if (context.Orders.SingleOrDefault(o => o.Id == order.Id) != null)
            {
                context.Orders.SingleOrDefault(o => o.Id == order.Id).Products.Clear();
            }

            context.SaveChanges();
        }

        public Order? GetOrderById(Guid orderId) 
        {
            return context.Orders.FirstOrDefault(o => o.Id == orderId);
        }

        public double CalculateTotalPrice(Order order) 
        {
            var myOrder = context.Orders.SingleOrDefault(o => o.Id == order.Id);

            double totalPrice = 0;
            if ( myOrder != null)
            {
                foreach (var product in myOrder.Products)
                {
                    totalPrice += product.Price;
                }
            }

            return totalPrice;
        }
    }
}

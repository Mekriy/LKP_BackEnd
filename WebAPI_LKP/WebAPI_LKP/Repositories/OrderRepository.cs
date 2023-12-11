using System.Data.Entity;
using WebAPI_LKP.Interfaces.Repositories;
using WebAPI_LKP.Models;

namespace WebAPI_LKP.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        readonly DbContexts.LkpContext context;
        public OrderRepository(DbContexts.LkpContext context)
        {
            this.context = context;
        }

        public async Task<bool> AddToOrder(Order order, Product product)
        {
            var myOrder = await context.Orders.FirstOrDefaultAsync(o => o.Id == order.Id);

            if (myOrder != null)
            {
                myOrder.ProductId = product.Id;
            }
   
            return await SaveAsync();
        }

        public async Task<bool> RemoveFromOrder(Order order, Product product)
        {
            var myOrder = await context.Orders.SingleOrDefaultAsync(o => o.Id == order.Id);

            var myProduct = await context.Products.SingleOrDefaultAsync(p => p.Id == product.Id);

            if (myOrder != null && myProduct != null) 
            {
                myOrder.ProductId = Guid.Empty;
            }

            return await SaveAsync();
        }

        public async Task<bool>ClearOrder(Order order)
        {
            var myOrder = await context.Orders.SingleOrDefaultAsync(o => o.Id == order.Id);

            if (myOrder != null)
            {
                myOrder.ProductId = Guid.Empty;
            }

            return await SaveAsync();
        }

        public async Task<Order?> GetOrderById(Guid orderId)
        {
            return await context.Orders.FirstOrDefaultAsync(o => o.Id == orderId);
        }

        public async Task<bool> SaveAsync()
        {
            var saved = await context.SaveChangesAsync();
            return saved > 0 ? true : false;
        }
        public async Task<bool> AddNewOrder(User user, Order order)
        {
            var myuser = await context.Users.SingleOrDefaultAsync(u => u.Id == user.Id);

            if (myuser == null)
            {
                return false;
            }

            myuser.Orders.Add(order);

            return await SaveAsync();
        }

        public async Task<bool> AddNewOrder(User user, List<Order> orders)
        {
            var myuser = await context.Users.SingleOrDefaultAsync(u => u.Id == user.Id);

            if (myuser == null)
            {
                return false;
            }

            myuser.Orders.AddRange(orders);

            return await SaveAsync();
        }

        public async Task<bool> RemoveUserOrder(User user, Order order)
        {
            var myuser = await context.Users.SingleOrDefaultAsync(u => u.Id == user.Id);

            if (myuser == null)
            {
                return false;
            }

            myuser.Orders.Remove(order);

            return await SaveAsync();
        }

        public async Task<bool> ClearUserOrders(User user)
        {
            var myuser = await context.Users.SingleOrDefaultAsync(u => u.Id == user.Id);

            if (myuser == null)
            {
                return false;
            }

            myuser.Orders.Clear();

            return await SaveAsync();
        }
    }
}

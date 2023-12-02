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

            //if (myOrder != null)
            //{
            //    myOrder.Products.Add(product);
            //}
   
            return await SaveAsync();
        }

        public async Task<bool> RemoveFromOrder(Order order, Product product)
        {
            var myOrder = await context.Orders.SingleOrDefaultAsync(o => o.Id == order.Id);

            var myProduct = await context.Products.SingleOrDefaultAsync(p => p.Id == product.Id && p.OrderId == product.OrderId);

            //if (myOrder != null && myProduct != null) 
            //{
            //    myOrder.Products.Remove(product);
            //}

            return await SaveAsync();
        }

        public async Task<bool>ClearOrder(Order order)
        {
            var myOrder = await context.Orders.SingleOrDefaultAsync(o => o.Id == order.Id);

            //if (myOrder != null)
            //{
            //    myOrder.Products.Clear();
            //}

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
    }
}

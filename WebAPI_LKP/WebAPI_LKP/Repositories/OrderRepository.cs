using MailKit.Search;
using Org.BouncyCastle.Asn1.X509;
using Microsoft.EntityFrameworkCore;
using WebAPI_LKP.Interfaces.Repositories;
using WebAPI_LKP.Models;
using Microsoft.AspNet.Identity;

namespace WebAPI_LKP.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        readonly DbContexts.LkpContext context;
        public OrderRepository(DbContexts.LkpContext context)
        {
            this.context = context;
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

        public async Task<bool> AddOrders(List<Order> orders)
        {
            context.Orders.AddRange(orders);
            return await SaveAsync();
        }

        public async Task<List<Order>> GetOrdersById(Guid userId)
        {
            return await context.Orders.Where(or => or.UserId == userId).ToListAsync();
        }

        public async Task<bool> UpdateOrder(Order order)
        {
            context.Orders.Update(order);
            return await SaveAsync();
        }

        public async Task<bool> RemoveOrder(Order order)
        {
            context.Orders.Remove(order);

            return await SaveAsync();
        }

        public async Task<bool> DeleteOrder(Guid orderId)
        {
            var order = await context.Orders.FirstOrDefaultAsync(o => o.Id == orderId);

            if (order != null)
            {
                context.Orders.Remove(order);
            }

            return await SaveAsync();
        }
    }
}

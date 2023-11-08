using WebAPI_LKP.Models;
using WebAPI_LKP.Interfaces.Repositories;
using System.Data.Entity;

namespace WebAPI_LKP.Repositories
{
    public class ProductRepository : IProductRepository
    {
        readonly DbContexts.LkpContext context;
        public ProductRepository(DbContexts.LkpContext context)
        {
            this.context = context;
        }

        public async Task<List<Product>> GetAllProducts()
        {
            return await context.Products.ToListAsync();
        }

        public async Task<Product?> GetProductById(Guid productId)
        {
            return await context.Products.FirstOrDefaultAsync(p => p.Id == productId);
        }
    }
}

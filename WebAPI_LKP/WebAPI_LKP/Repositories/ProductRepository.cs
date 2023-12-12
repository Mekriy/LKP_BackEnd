using WebAPI_LKP.Models;
using WebAPI_LKP.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using WebAPI_LKP.DbContexts;

namespace WebAPI_LKP.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly LkpContext _context;
        public ProductRepository(LkpContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAllProducts()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product?> GetProductById(Guid productId)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);
        }
    }
}

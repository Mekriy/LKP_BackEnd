using WebAPI_LKP.Models;
using WebAPI_LKP.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using WebAPI_LKP.DbContexts;
using Microsoft.EntityFrameworkCore.Infrastructure;
using WebAPI_LKP.DTO;

namespace WebAPI_LKP.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly LkpContext _context;
        public ProductRepository(LkpContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateProducts(List<Product> products)
        {
            await _context.Products.AddRangeAsync(products);
            return await SaveAsync();
        }

        public async Task<List<Product>> GetAllProducts()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product?> GetProductById(Guid productId)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);
        }

        public async Task<List<ProductToDeliverDTO>> GetProductToDeliver()
        {
            var query = _context.Products
                    .Join(_context.Orders, ps => ps.Id, os => os.ProductId, (ps, os) => new { ps, os })
                    .Join(_context.Users, joined => joined.os.UserId, aus => aus.Id, (joined, aus) => new { joined, aus })
                    .Select(result => new ProductToDeliverDTO
                    {
                        Email = result.aus.Email,
                        ProductName = result.joined.ps.ProductName,
                        Quantity = result.joined.os.Quantity,
                        Delivery = result.joined.os.Delivery,
                        Price = result.joined.ps.Price
                    });

            return await query.ToListAsync();
        }

        public async Task<bool> SaveAsync()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0 ? true : false;
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            _context.Products.Update(product);
            return await SaveAsync();
        }
        public async Task<bool> DeleteProduct(Guid productId)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);

            if(product != null)
            {
                _context.Products.Remove(product);
            }

            return await SaveAsync();
        }
    }
}

using WebAPI_LKP.Models;
using WebAPI_LKP.Interfaces;

namespace WebAPI_LKP.Repositories
{
    public class ProductRepository : IProductRepository
    {
        readonly DbContexts.AppContext context;
        public ProductRepository(DbContexts.AppContext context)
        {
            this.context = context;
        }

        public IEnumerable<Product> AllProducts 
        { 
            get 
            {
                return context.Products.Where(p => p.IsAvailable == true);
            }
        }

        public Product? GetProductById(Guid productId)
        {
            return AllProducts.FirstOrDefault(p => p.Id == productId);
        }

    }
}

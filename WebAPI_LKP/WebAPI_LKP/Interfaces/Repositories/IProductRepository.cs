using WebAPI_LKP.Models;

namespace WebAPI_LKP.Interfaces.Repositories
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllProducts();
        Task<Product?> GetProductById(Guid productId);
    }
}

using WebAPI_LKP.DTO;
using WebAPI_LKP.Models;

namespace WebAPI_LKP.Interfaces.Repositories
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllProducts();
        Task<Product?> GetProductById(Guid productId);
        Task<List<ProductToDeliverDTO>> GetProductToDeliver();
        Task<bool> CreateProducts(List<Product> products);
        Task<bool> UpdateProduct(Product product);
        Task<bool> DeleteProduct(Guid productId);
        Task<bool> SaveAsync();
    }
}

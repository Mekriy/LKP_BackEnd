using WebAPI_LKP.DTO;
using WebAPI_LKP.Models;

namespace WebAPI_LKP.Interfaces.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetAllProducts();
        Task<List<ProductToDeliverDTO>> GetProductsToDeliver();
        Task<bool> CreateProduct(List<ProductDTO> products);
        Task<bool> UpdateProduct(ProductDTO product);
        Task<bool> DeleteProduct(Guid productId);
    }
}

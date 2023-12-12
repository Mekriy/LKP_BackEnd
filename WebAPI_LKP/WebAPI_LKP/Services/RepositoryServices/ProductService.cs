using WebAPI_LKP.Interfaces.Repositories;
using WebAPI_LKP.Interfaces.Services;
using WebAPI_LKP.Models;

namespace WebAPI_LKP.Services.RepositoryServices
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<List<Product>> GetAllProducts()
        {
            return await _productRepository.GetAllProducts();
        }
    }
}

using WebAPI_LKP.DbContexts;
using WebAPI_LKP.DTO;
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

        public Task<bool> CreateProduct(List<ProductDTO> products)
        {
            var myproducts = products.Select(productdto => new Product
            {
                Id = productdto.Id,
                Description = productdto.Description,
                Image = productdto.Image,
                Price = productdto.Price,
                ProductName = productdto.ProductName,
                Type = productdto.Type
            }).ToList();

            return _productRepository.CreateProducts(myproducts);
        }

        public async Task<List<Product>> GetAllProducts()
        {
            return await _productRepository.GetAllProducts();
        }

        public async Task<List<ProductToDeliverDTO>> GetProductsToDeliver()
        {
            return await _productRepository.GetProductToDeliver();
        }

        public async Task<bool> UpdateProduct(ProductDTO product)
        {
            var myproducts = new Product
            {
                Id = product.Id,
                Description = product.Description,
                Image = product.Image,
                Price = product.Price,
                ProductName = product.ProductName,
                Type = product.Type
            };

            return await _productRepository.UpdateProduct(myproducts);
        }
        public Task<bool> DeleteProduct(Guid productId)
        {
            return _productRepository.DeleteProduct(productId);
        }
    }
}

using WebAPI_LKP.Models;

namespace WebAPI_LKP.Interfaces.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetAllProducts();
    }
}

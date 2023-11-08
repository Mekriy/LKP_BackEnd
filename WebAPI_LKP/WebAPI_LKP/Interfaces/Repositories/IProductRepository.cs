using WebAPI_LKP.Models;

namespace WebAPI_LKP.Interfaces.Repositories
{
    public interface IProductRepository
    {
        IEnumerable<Product> AllProducts { get; }
        Product? GetProductById(Guid id);
    }
}

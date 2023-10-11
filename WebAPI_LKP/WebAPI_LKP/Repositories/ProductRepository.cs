using WebAPI_LKP.Models;

namespace WebAPI_LKP.Repositories
{
    public class ProductRepository
    {
        public IEnumerable<Product> AllProducts =>
            new List<Product>
            {
                new Product() {ProductName = "Tom Yam", Price = 150, Image = " "},
                new Product() {ProductName = "Margarita", Price = 160, Image = " "},
                new Product() {ProductName = "Salami&Telatina", Price = 200, Image = " "},
                new Product() {ProductName = "Carbonara", Price = 390, Image = " "},
                new Product() {ProductName = "Peperoni light", Price = 290, Image = " "}
            };

        public Product? GetProductById(Guid productId)
        {
            return AllProducts.FirstOrDefault(p => p.Id == productId);
        }
    }
}

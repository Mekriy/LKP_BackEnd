using System.IO.Pipelines;
using WebAPI_LKP.Models;

namespace WebAPI_LKP.DbContexts
{
    public class DbInitializer
    {
        public static void SeedProducts(IApplicationBuilder applicationBuilder)
        {
            AppContext context = applicationBuilder.ApplicationServices.CreateScope().
                ServiceProvider.GetRequiredService<AppContext>();

            if (!context.Products.Any())
            {
                context.AddRange
                (
                    new Product() { ProductName = "Tom Yam", Price = 150, Image = " " },
                    new Product() { ProductName = "Margarita", Price = 160, Image = " " },
                    new Product() { ProductName = "Salami&Telatina", Price = 200, Image = " " },
                    new Product() { ProductName = "Carbonara", Price = 390, Image = " " },
                    new Product() { ProductName = "Peperoni light", Price = 290, Image = " " }
                );
            }

            context.SaveChanges();
        }

        public static void SeedAdmins(IApplicationBuilder applicationBuilder)
        {
            AppContext context = applicationBuilder.ApplicationServices.CreateScope().
                ServiceProvider.GetRequiredService<AppContext>();

            if (!context.Users.Where(u => u.IsAdmin == true).Any())
            {
                context.AddRange
                (
                    new User() { Name = "Admin", Email = "oleosad@gmail.com", Password = "admin", IsAdmin = true }
                );
            }

            context.SaveChanges();
        }
    }
}

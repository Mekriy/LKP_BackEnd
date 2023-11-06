using System.IO.Pipelines;
using WebAPI_LKP.Models;

namespace WebAPI_LKP.DbContexts
{
    public class DbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            //AppContext context = applicationBuilder.ApplicationServices.CreateScope().
            //    ServiceProvider.GetRequiredService<AppContext>();

            AppContext context = new AppContext();

            if (!context.Products.Any())
            {
                context.AddRange
                (
                    new Product() { ProductName = "Tom Yam", Price = 260, Description = "Тісто з чотирьох видів борошна, соус на основі збитих вершків, моцарела, паста «Том Ям»", Image = "https://php.ninjapizza.com.ua/images/5/l2x.webp?ver=v1.0.7" },
                    new Product() { ProductName = "Margarita", Price = 280, Description = "Тісто з чотирьох видів борошна, перетерті томати пелаті, моцарела, помідор чері", Image = "https://php.ninjapizza.com.ua/images/9/l2x.webp?ver=v1.0.7" },
                    new Product() { ProductName = "Salami&Telatina", Price = 255, Description = "Тісто з чотирьох видів борошна, сухий часник, маслини, соус «Ворчестер»", Image = "https://php.ninjapizza.com.ua/images/24/l2x.webp?ver=v1.0.7" },
                    new Product() { ProductName = "Carbonara", Price = 350, Description = "Тісто з чотирьох видів борошна, моцарела, пармезан, цибуля фрі, бекон", Image = "https://php.ninjapizza.com.ua/images/21/l2x.webp?ver=v1.0.7" },
                    new Product() { ProductName = "Peporni light", Price = 400, Description = "Тісто з чотирьох видів борошна, перетерті томати пелаті, моцарела, салямі «Наполі»", Image = "https://php.ninjapizza.com.ua/images/1/l2x.webp?ver=v1.0.7" },
           
                    new Product() { ProductName = "Coca-Cola Vanilla 0.33", Price = 50, Description = "Coca-Cola Vanilla - прохолодний газований напій від бренду Coca-Cola з насиченим ванільним смаком", Image = "https://php.ninjapizza.com.ua/images/134/l2x.webp?ver=v1.0.7" },
                    new Product() { ProductName = "Лимонад Rose Fentimans", Price = 120, Description = "Створений із чистої рожевої олії, отриманої в болгарській долині Казанлик та свіжого лимонного соку", Image = "https://php.ninjapizza.com.ua/images/128/l2x.webp?ver=v1.0.7" }, 
                    new Product() { ProductName = "Coca-Cola", Price = 40, Description = "Цей напій магія смаку, неповторний смак, унікальна рецептура, неповторна форма упаковки", Image = "https://php.ninjapizza.com.ua/images/128/l2x.webp?ver=v1.0.7" },
                    new Product() { ProductName = "Живчик", Price = 35, Description = "Містить натуральний яблучний сік і екстракт ехінацеї. Напій збагачений мікроелементами та вітамінами", Image = "https://content.silpo.ua/sku/ecommerce/0/480x480wwm/1158_480x480wwm_a1d28fa7-936a-6a28-1853-8b556d306842.png" }
                    //new Product() { ProductName = "", Price = 0, Description = "", Image = "", Order = null, OrderId = null }
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

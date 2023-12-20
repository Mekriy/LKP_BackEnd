using System.IO.Pipelines;
using WebAPI_LKP.Models;
using WebAPI_LKP.Models.Enums;

namespace WebAPI_LKP.DbContexts
{
    public class DbInitializer
    {
        readonly LkpContext context;
        public DbInitializer()
        {
            context = new LkpContext();
        }

        public void SeedProducts()
        {
            if (!context.Products.Any())
            {
                var products = new List<Product>()
                {
                    new Product() { ProductName = "Tom Yam", Price = 260, Type="pizza", Description = "Тісто з чотирьох видів борошна, соус на основі збитих вершків, моцарела, паста «Том Ям»", Image = "https://php.ninjapizza.com.ua/images/5/l2x.webp?ver=v1.0.7" },
                    new Product() { ProductName = "Margarita", Price = 280, Type="pizza", Description = "Тісто з чотирьох видів борошна, перетерті томати пелаті, моцарела, помідор чері", Image = "https://php.ninjapizza.com.ua/images/9/l2x.webp?ver=v1.0.7" },
                    new Product() { ProductName = "Salami&Telatina", Price = 255, Type="pizza", Description = "Тісто з чотирьох видів борошна, сухий часник, маслини, соус «Ворчестер»", Image = "https://php.ninjapizza.com.ua/images/24/l2x.webp?ver=v1.0.7" },
                    new Product() { ProductName = "Carbonara", Price = 350, Type="pizza", Description = "Тісто з чотирьох видів борошна, моцарела, пармезан, цибуля фрі, бекон", Image = "https://php.ninjapizza.com.ua/images/21/l2x.webp?ver=v1.0.7" },
                    new Product() { ProductName = "Peporni light", Price = 400, Type="pizza", Description = "Тісто з чотирьох видів борошна, перетерті томати пелаті, моцарела, салямі «Наполі»", Image = "https://php.ninjapizza.com.ua/images/1/l2x.webp?ver=v1.0.7" },

                    new Product() { ProductName = "Піца з креветками і солодким чилі", Price = 320, Type="pizza", Description = "Рибний соус, тісто з чотирьох видів борошна, моцарела, сир з червоним песто", Image = "https://php.ninjapizza.com.ua/images/1/l2x.webp?ver=v1.0.7" },
                    new Product() { ProductName = "Пепероні з гострим медом", Price = 345, Type="pizza", Description = "Тісто з чотирьох видів борошна, перетерті томати пелаті, моцарела, пепероні", Image = "https://php.ninjapizza.com.ua/images/10/l2x.webp?ver=v1.0.7" },
                    new Product() { ProductName = "Кватро Формаджі з цибулевим мармеладом", Price = 360, Type="pizza", Description = "Тісто з чотирьох видів борошна, соус на основі збитих вершків, моцарела, сир з червоним песто", Image = "https://php.ninjapizza.com.ua/images/12/l2x.webp?ver=v1.0.7" },
                    new Product() { ProductName = "Піца з ковбасками і чеддером фламбе", Price = 390, Type="pizza", Description = "Тісто з чотирьох видів борошна, перетерті томати пелаті, моцарела, ковбаски", Image = "https://php.ninjapizza.com.ua/images/4/l2x.webp?ver=v1.0.7" },
                    new Product() { ProductName = "Піца з італійським беконом і грибами", Price = 310, Type="pizza", Description = "Тісто з чотирьох видів борошна, моцарела, пармезан, соус на основі збитих вершків", Image = "https://php.ninjapizza.com.ua/images/20/l2x.webp?ver=v1.0.7" },
                    new Product() { ProductName = "Піца по-селянськи з беконом", Price = 300, Type="pizza", Description = "Тісто з чотирьох видів борошна, моцарела, пармезан, соус на основі збитих вершків", Image = "https://php.ninjapizza.com.ua/images/23/l2x.webp?ver=v1.0.7" },
                    new Product() { ProductName = "Піца з пряним суджуком і рікотою", Price = 370, Type="pizza", Description = "Тісто з чотирьох видів борошна, моцарела, пармезан, соус на основі збитих вершків", Image = "https://php.ninjapizza.com.ua/images/25/l2x.webp?ver=v1.0.7" },

                    new Product() { ProductName = "Coca-Cola Vanilla 0.33", Price = 50, Type="drink", Description = "Coca-Cola Vanilla - прохолодний газований напій від бренду Coca-Cola з насиченим ванільним смаком", Image = "https://php.ninjapizza.com.ua/images/134/l2x.webp?ver=v1.0.7" },
                    new Product() { ProductName = "Лимонад Rose Fentimans", Price = 120, Type = "pizza", Description = "Створений із чистої рожевої олії, отриманої в болгарській долині Казанлик та свіжого лимонного соку", Image = "https://php.ninjapizza.com.ua/images/128/l2x.webp?ver=v1.0.7" },
                    new Product() { ProductName = "Coca-Cola", Price = 40, Type = "pizza", Description = "Цей напій магія смаку, неповторний смак, унікальна рецептура, неповторна форма упаковки", Image = "https://php.ninjapizza.com.ua/images/101/l2x.webp?ver=v1.0.7" },
                    new Product() { ProductName = "Живчик", Price = 35, Type="drink", Description = "Містить натуральний яблучний сік і екстракт ехінацеї. Напій збагачений мікроелементами та вітамінами", Image = "https://content.silpo.ua/sku/ecommerce/0/480x480wwm/1158_480x480wwm_a1d28fa7-936a-6a28-1853-8b556d306842.png" }
                };

                context.Products.AddRange(products);
                context.SaveChanges();
            }
        }

        public void SeedUsers()
        {
            if (!context.Users.Any())
            {
                var users = new List<User>()
                {
                    new User() {UserName="Oles", Email="oles@gamil.com", PasswordHash="P@ssword123", Role = Roles.Admin},
                    new User() {UserName="Andriy", Email="andriy@gamil.com", PasswordHash="P@ssword123", Role = Roles.Admin},
                    new User() {UserName="Dima", Email="dima@gamil.com", PasswordHash="P@ssword123", Role = Roles.Admin}
                };

                context.Users.AddRange(users);
                context.SaveChanges();
            }
        }

    }
}

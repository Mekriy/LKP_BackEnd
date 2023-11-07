using Microsoft.EntityFrameworkCore;
using WebAPI_LKP.Models;

namespace WebAPI_LKP.DbContexts
{
    public class AppContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Олесь
            //optionsBuilder.UseSqlite("Data Source=D:\\STUDY3\\ЛАБИ\\проект\\Dzhgut\\LKP_BackEnd\\WebAPI_LKP\\DzhgutDb.db");
            // Андрій
            optionsBuilder.UseSqlite("Data Source=C:\\NLTU\\My nltu reports\\3rd course\\1 sem\\УІТП\\project\\LKP_BackEnd\\WebAPI_LKP\\DzhgutDb.db");
        }
        public AppContext(DbContextOptions<AppContext> options) : base(options) { }
        public AppContext() { }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}

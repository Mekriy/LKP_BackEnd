using Microsoft.EntityFrameworkCore;

namespace WebAPI_LKP.Models
{
    public class AppContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=D:\\STUDY3\\ЛАБИ\\проект\\Dzhgut\\LKP_BackEnd\\WebAPI_LKP\\DzhgutDb.db");
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;
using WebAPI_LKP.Models;

namespace WebAPI_LKP.DbContexts
{
    public class LkpContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Олесь
            //optionsBuilder.UseMySQL("Server=localhost;database=DzhgutDb;user=root;password=root");
            //Andrii
            optionsBuilder.UseMySQL("Server=localhost;database=DzhgutDb;user=root;password=MyPassword54321");
        }
        public LkpContext(DbContextOptions<LkpContext> options) : base(options) { }
        public LkpContext() { }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WebAPI_LKP.Models;

namespace WebAPI_LKP.DbContexts
{
    public class LkpContext : DbContext
    {
        public LkpContext(DbContextOptions<LkpContext> options) : base(options) { }
        public LkpContext() { }

        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Token> Tokens { get; set; }
    }
}
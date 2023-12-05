using WebAPI_LKP.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using WebAPI_LKP.Models.Tokens;

namespace WebAPI_LKP.DbContexts
{
    public class LkpContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public LkpContext(DbContextOptions<LkpContext> options) : base(options) { }
        public LkpContext() { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("INSERT DB LINK HERE");
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Token> Tokens { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
    }
}
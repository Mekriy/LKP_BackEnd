using MailKit.Search;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.X509;
using WebAPI_LKP.Interfaces.Repositories;
using WebAPI_LKP.Models;
using WebAPI_LKP.Models.Tokens;

namespace WebAPI_LKP.Repositories
{
    public class UserRepository : IUserRepository
    {
        readonly DbContexts.LkpContext context;
        public UserRepository()
        {
            context = new DbContexts.LkpContext();
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await context.Users.ToListAsync();
        }

        public async Task<User> GetUser(string email)
        {
            return await context.Users.Where(u => u.Email == email).FirstOrDefaultAsync();
        }

        public async Task<User?> GetUserById(Guid userId) 
        {
            return await context.Users.FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<bool> DoUserExist(string email, string password)
        {
            return await context.Users.AnyAsync(u => u.Email == email && u.PasswordHash == password);
        }
        public async Task<bool> AddUser(User user)
        {
            context.Users.Add(user);
            return await SaveAsync();
        }

        public async Task<bool> SaveAsync()
        {
            var saved = await context.SaveChangesAsync();
            return saved > 0 ? true : false;
        }
        public async Task<bool> CreateRefreshToken(RefreshToken refreshToken)
        {
            context.RefreshTokens.Add(refreshToken);
            return await SaveAsync();
        }

        public async Task<RefreshToken?> FindRefreshToken(string token)
        {
            return await context.RefreshTokens.FirstOrDefaultAsync(t => t.Token == token);
        }

        public async Task<bool> UpdateRefreshToken(RefreshToken refreshToken)
        {
            context.RefreshTokens.Update(refreshToken);
            return await SaveAsync();
        }

        public async Task<List<RefreshToken?>> GetAllRefreshTokensByUserId(string userId)
        {
            return await context.RefreshTokens.Where(rt => rt.UserId == userId).ToListAsync();
        }
    }
}
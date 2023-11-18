using Microsoft.EntityFrameworkCore;
using WebAPI_LKP.Interfaces.Repositories;
using WebAPI_LKP.Models;

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
            return await context.Users.AnyAsync(u => u.Email == email && u.Password == password);
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
    }
}
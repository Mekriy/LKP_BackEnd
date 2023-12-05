using WebAPI_LKP.Models;
using WebAPI_LKP.Models.Tokens;

namespace WebAPI_LKP.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUsers();
        Task<User> GetUser(string email);
        Task<User?> GetUserById(Guid userId);
        Task<bool> DoUserExist(string email, string password);
        Task<bool> AddUser(User user);
        Task<bool> SaveAsync();
        Task<bool> CreateRefreshToken(RefreshToken refreshToken);
        Task<RefreshToken?> FindRefreshToken(string token);
        Task<bool> UpdateRefreshToken(RefreshToken refreshToken);
    }
}

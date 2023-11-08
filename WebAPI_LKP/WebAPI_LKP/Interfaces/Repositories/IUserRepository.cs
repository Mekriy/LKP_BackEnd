using WebAPI_LKP.Models;

namespace WebAPI_LKP.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUsers();
        Task<User> GetUser(string email);
        Task<User?> GetUserById(Guid userId);
        Task<bool> DoUserExist(string email, string password);
        Task<bool> SaveAsync();
    }
}

using WebAPI_LKP.Models;

namespace WebAPI_LKP.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<User> AllUsers { get; }
        bool DoUserExist(User user);
        User? GetUserById(Guid userId);
    }
}

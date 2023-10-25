using WebAPI_LKP.Interfaces;
using WebAPI_LKP.Models;

namespace WebAPI_LKP.Repositories
{
    public class UserRepository : IUserRepository
    {
        readonly DbContexts.AppContext context;
        public UserRepository()
        {
            context = new DbContexts.AppContext();
        }
        public IEnumerable<User> AllUsers
        {
            get
            {
                return context.Users.Where(u => u != null);
            }
        }

        public IEnumerable<User> AllAdmins
        {
            get
            {
                return AllUsers.Where(u => u.IsAdmin == true);
            }
        }

        public bool DoUserExist(User user)
        {
            return true; // доробити :)
        }

        public User? GetUserById(Guid userId) 
        {
            AllUsers.FirstOrDefault(u => u.Id == userId);
            return null; 
        }
    }
}

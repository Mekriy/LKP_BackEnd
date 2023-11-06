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

        public bool DoUserExist(User user)
        {
            bool flag = false;
            foreach (var u in AllUsers) 
            {
                if (u.Id == user.Id) { flag = true; break; }
            }
            return flag;
        }

        public User? GetUserById(Guid userId) 
        {
            return AllUsers.FirstOrDefault(u => u.Id == userId);
        }
    }
}

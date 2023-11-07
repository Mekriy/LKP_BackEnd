using WebAPI_LKP.DTO;
using WebAPI_LKP.Interfaces.Repositories;
using WebAPI_LKP.Interfaces.Services;
using WebAPI_LKP.Models;

namespace WebAPI_LKP.Services.RepositoryServices
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<IEnumerable<User>> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public Task<User?> GetUser(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<User?> GetUserByGuid(Guid UserGuid)
        {
            return _userRepository.GetUserById(UserGuid);
        }

        public Task<UserDTO?> GetUserDTO(string email)
        {
            throw new NotImplementedException();
        }

        public Task<bool> NameOrEmailCheck(string name, string email)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UserExist(string email, string password)
        {
            throw new NotImplementedException();
        }
    }
}

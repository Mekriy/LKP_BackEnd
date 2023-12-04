using WebAPI_LKP.DTO;
using WebAPI_LKP.Models;

namespace WebAPI_LKP.Interfaces.Services
{
    public interface IUserService
    {
        Task<User?> GetUser(string email);
        Task<UserLoginDTO?> GetUserDTO(string email);
        Task<User?> GetUserByGuid(string UserGuid);
        Task<List<User>> GetAllUsers();
        Task<List<UserLoginDTO>> GetAllUsersDTO();

        Task<bool> CreateUser(UserSignUpDTO userDto);
        Task<bool> UserExist(string email);
        Task<bool> CheckPassword(User user, string password);
        Task<bool> NameOrEmailCheck(string name, string email);
        Task<string> GenerateJwtToken(User user);
    }
}

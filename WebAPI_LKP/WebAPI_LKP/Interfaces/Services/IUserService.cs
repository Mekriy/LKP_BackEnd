using WebAPI_LKP.DTO;
using WebAPI_LKP.Models;

namespace WebAPI_LKP.Interfaces.Services
{
    public interface IUserService
    {
        Task<User?> GetUser(string email);
        Task<UserDTO?> GetUserDTO(string email);
        Task<User?> GetUserByGuid(Guid UserGuid);
        Task<List<User>> GetAllUsers();
        Task<List<UserDTO>> GetAllUsersDTO();

        Task<bool> CreateUser(UserDTO userDto);
        string HashPassword(string password);
        Task<bool> UserExist(string email, string password);
        Task<bool> NameOrEmailCheck(string name, string email);
    }
}

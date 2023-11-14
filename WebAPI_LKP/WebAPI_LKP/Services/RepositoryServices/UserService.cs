using AutoMapper;
using WebAPI_LKP.DTO;
using WebAPI_LKP.Interfaces.Repositories;
using WebAPI_LKP.Interfaces.Services;
using WebAPI_LKP.Models;

namespace WebAPI_LKP.Services.RepositoryServices
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(
            IUserRepository userRepository,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<List<User>> GetAllUsers()
        {
            return await _userRepository.GetAllUsers();
        }
        public async Task<List<UserDTO>> GetAllUsersDTO()
        {
            var users = await _userRepository.GetAllUsers();
            var usersDto = _mapper.Map<List<UserDTO>>(users);
            return usersDto;
        }
        public async Task<User?> GetUser(string email)
        {
            return await _userRepository.GetUser(email);
        }
        public async Task<User?> GetUserByGuid(Guid UserGuid)
        {
            return await _userRepository.GetUserById(UserGuid);
        }
        public async Task<UserDTO?> GetUserDTO(string email)
        {
            var user = await _userRepository.GetUser(email);
            var userDto = _mapper.Map<UserDTO>(user);
            return userDto;
        }
        public async Task<bool> NameOrEmailCheck(string name, string email)
        {
            var user = await _userRepository.GetUser(email);
            if(user==null)
                return false;
            if(user.Email== email || user.Name == name)
            {
                return true;
            }
            return false;

        }
        public async Task<bool> UserExist(string email, string password)
        {
            var PasswordHash = HashPassword(password);
            return await _userRepository.DoUserExist(email, PasswordHash);
            //return await _userRepository.DoUserExist(email, password);
        }
        public async Task<bool> CreateUser(UserDTO userDto)
        {
            var user = _mapper.Map<User>(userDto);
            return await _userRepository.AddUser(user);
        }
        public string HashPassword(string password)
        {
            var PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);
            return PasswordHash;
        }
    }
}

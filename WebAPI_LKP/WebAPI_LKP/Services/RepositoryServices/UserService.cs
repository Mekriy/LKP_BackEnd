using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPI_LKP.DbContexts;
using WebAPI_LKP.DTO;
using WebAPI_LKP.Interfaces.Repositories;
using WebAPI_LKP.Interfaces.Services;
using WebAPI_LKP.Models;
using WebAPI_LKP.Models.Enums;

namespace WebAPI_LKP.Services.RepositoryServices
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly LkpContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        public UserService(
            IUserRepository userRepository,
            IMapper mapper,
            LkpContext context,
            UserManager<User> userManager,
            IConfiguration configuration)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _context = context;
            _userManager = userManager;
            _configuration = configuration;
        }
        public async Task<List<User>> GetAllUsers()
        {
            return await _userRepository.GetAllUsers();
        }
        public async Task<List<UserLoginDTO>> GetAllUsersDTO()
        {
            var users = await _userRepository.GetAllUsers();
            var usersDto = _mapper.Map<List<UserLoginDTO>>(users);
            return usersDto;
        }
        public async Task<User?> GetUser(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }
        public async Task<User?> GetUserByGuid(string UserGuid)
        {
            return await _userManager.FindByIdAsync(UserGuid);
        }
        public async Task<UserLoginDTO?> GetUserDTO(string email)
        {
            var user = await _userRepository.GetUser(email);
            var userDto = _mapper.Map<UserLoginDTO>(user);
            return userDto;
        }
        public async Task<bool> NameOrEmailCheck(string name, string email)
        {
            var user = await _userRepository.GetUser(email);
            if(user==null)
                return false;
            if(user.Email== email || user.UserName == name)
            {
                return true;
            }
            return false;

        }
        public async Task<bool> UserExist(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
                return true;
            else
                return false;
        }
        public async Task<bool> CheckPassword(User user, string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }
        public async Task<bool> CreateUser(UserSignUpDTO userSignUp)
        {
            var user = new User()
            {
                UserName = userSignUp.Name,
                Email = userSignUp.Email,
                Role = Roles.User,
                EmailConfirmed = false
            };
            var isCreated = await _userManager.CreateAsync(user, userSignUp.Password);
            if (isCreated.Succeeded)
                return true;
            else
                return false;
        }
        public async Task<string> GenerateJwtToken(User user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration.GetSection("JWT:Key").Value);

            var tokenDescripter = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Email, value: user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, value: DateTime.Now.ToUniversalTime().ToString())
                }),
                Expires = DateTime.Now.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescripter);

            return jwtTokenHandler.WriteToken(token);
        }
    }
}

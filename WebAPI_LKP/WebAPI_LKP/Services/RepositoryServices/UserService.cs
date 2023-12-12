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
using WebAPI_LKP.Models.Tokens;
using WebAPI_LKP.Models.Enums;
using WebAPI_LKP.Services.Authentication;

namespace WebAPI_LKP.Services.RepositoryServices
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly LkpContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly TokenValidationParameters _tokenValidationParameters;
        public UserService(
            IUserRepository userRepository,
            IMapper mapper,
            LkpContext context,
            UserManager<User> userManager,
            IConfiguration configuration,
            TokenValidationParameters tokenValidationParameters)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _context = context;
            _userManager = userManager;
            _configuration = configuration;
            _tokenValidationParameters = tokenValidationParameters;
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
                Role = Roles.Admin,
                EmailConfirmed = false
            };
            var isCreated = await _userManager.CreateAsync(user, userSignUp.Password);
            if (isCreated.Succeeded)
                return true;
            else
                return false;
        }
        public async Task<AuthResult> GenerateJwtToken(User user)
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
                    new Claim(JwtRegisteredClaimNames.Iat, value: DateTime.Now.ToUniversalTime().ToString()),
                }),
                Expires = DateTime.UtcNow.Add(TimeSpan.Parse(_configuration.GetSection("JWT:ExpiryTimeFrame").Value)),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescripter);
            var jwtToken = jwtTokenHandler.WriteToken(token);

            var refreshToken = new RefreshToken()
            {
                JwtId = token.Id,
                Token = RandomStringGeneration(23),
                AddedDate = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddMonths(6),
                IsRevoked = false,
                IsUsed = false,
                UserId = user.Id.ToString()
            };

            await _userRepository.CreateRefreshToken(refreshToken);
            
            return new AuthResult()
            {
                Token = jwtToken,
                RefreshToken = refreshToken.Token,
                Result = true
            };
        }
        public string RandomStringGeneration(int length)
        {
            var random = new Random();
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890abcdefghijklmnopqrstuvwxyz_";
            return new string(Enumerable.Repeat(chars, length).Select(x => x[random.Next(x.Length)]).ToArray());

        }
        public async Task<AuthResult> VerifyAndGenerateToken(TokenRequest tokenRequest)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var tokenInVerification = jwtTokenHandler.ValidateToken(tokenRequest.Token, 
                    _tokenValidationParameters,
                    out var validatedToken);

                if(validatedToken is JwtSecurityToken jwtSecurityToken)
                {
                    var result = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, 
                        StringComparison.InvariantCultureIgnoreCase);
                    if (result == false)
                        return null;
                }

                var UtcExpiryDate = long.Parse(tokenInVerification.Claims.
                    FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp).Value);

                var expiryDate = UnixTimeStampToDateTime(UtcExpiryDate);
                if (expiryDate > DateTime.Now)
                    return new AuthResult()
                    {
                        Result = false,
                        Errors = new List<string>()
                        {
                            "Expired token"
                        }
                    };

                var storedToken = await _userRepository.FindRefreshToken(tokenRequest.RefreshToken);
                
                 if(storedToken == null)
                   return new AuthResult()
                    {
                        Result = false,
                        Errors = new List<string>()
                        {
                            "Invalid tokens"
                        }
                    };
                 
                
                 if(storedToken.IsUsed)
                    return new AuthResult()
                    {
                        Result = false,
                        Errors = new List<string>()
                        {
                            "Token is already used"
                        }
                    };
                if(storedToken.IsRevoked)
                    return new AuthResult()
                    {
                        Result = false,
                        Errors = new List<string>()
                        {
                            "Token is already revoked"
                        }
                    };
                var jti = tokenInVerification.Claims.
                    FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti).Value;

                if(storedToken.JwtId != jti)
                    return new AuthResult()
                    {
                        Result = false,
                        Errors = new List<string>()
                        {
                            "Token id is invalid"
                        }
                    };
                if(storedToken.ExpiryDate < DateTime.UtcNow)
                    return new AuthResult()
                    {
                        Result = false,
                        Errors = new List<string>()
                        {
                            "Expired tokens"
                        }
                    };

                storedToken.IsUsed = true;
                await _userRepository.UpdateRefreshToken(storedToken);
                
                var dbUser = await _userManager.FindByIdAsync(storedToken.UserId);
                return await GenerateJwtToken(dbUser);
            }
            catch (Exception)
            {
                return new AuthResult()
                {
                    Result = false,
                    Errors = new List<string>()
                        {
                            "Server error while re-generating tokens"
                        }
                };
            }
        }
        public DateTime UnixTimeStampToDateTime(long unixTimeStamp)
        {
            var dateTimeVal = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTimeVal = dateTimeVal.AddSeconds(unixTimeStamp).ToUniversalTime();

            return dateTimeVal;
        }

        public async Task<bool> RevokeRefreshToken(User user)
        {
            var refreshTokens = await _userRepository.GetAllRefreshTokensByUserId(user.Id.ToString());
            foreach(var refreshToken in refreshTokens)
            {
                refreshToken.IsRevoked = true;
                if (!await _userRepository.UpdateRefreshToken(refreshToken))
                    return false;
            }
            return true;
        }
    }
}

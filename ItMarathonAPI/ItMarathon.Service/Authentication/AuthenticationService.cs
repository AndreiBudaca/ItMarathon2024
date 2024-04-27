using ItMarathon.Core;
using ItMarathon.Core.CustomClaims;
using ItMarathon.Data.Entities;
using ItMarathon.Data.Infrastructure;
using ItMarathon.Service.Authentication.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ItMarathon.Service.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IRepository<User> userRepository;
        private readonly IUnitOfWork unitOfWork;

        public AuthenticationService(IRepository<User> userRepository, IUnitOfWork unitOfWork)
        {
            this.userRepository = userRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<UserDto?> LoginAsync(LoginUserDto login)
        {
            var dbUser = await userRepository.Query()
                .Where(u => u.Email == login.Email)
                .FirstOrDefaultAsync();

            if (dbUser == null) return null;

            var dbUserHashString = Convert.ToBase64String(dbUser.PasswordHash);
            var loginHashString = Convert.ToBase64String(SHA256.HashData(Encoding.ASCII.GetBytes(login.Password)));
            
            if (dbUserHashString != loginHashString) 
                return null;

            return new UserDto
            {
                Id = dbUser.Id,
                Email = dbUser.Email,
                LastName = dbUser.LastName,
                FirstName = dbUser.FirstName,
                YearOfStudy = dbUser.YearOfStudy,
                Semester = dbUser.Semester,
                Role = dbUser.Role,
            };
        }

        public async Task<string> RegisterAsync(CreateUserDto user)
        {
            var conflictingUser = await userRepository.Query()
                .Where(u => u.Email == user.Email)
                .FirstOrDefaultAsync();

            if (conflictingUser != null)
            {
                return "An user with this email already exists";
            }

            userRepository.Add(new User
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Role = user.Role,
                YearOfStudy = user.YearOfStudy,
                Semester = user.Semester,
                PasswordHash = SHA256.HashData(Encoding.ASCII.GetBytes(user.Password))
            });

            await unitOfWork.CommitAsync();

            return string.Empty;
        }

        public string GenerateJSONWebToken(UserDto userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppConfig.JwtSecret));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, userInfo.Email),
                new Claim(ClaimTypes.Role, userInfo.Role.ToString()),
                new Claim(CustomClaims.YearOfStudy, userInfo.YearOfStudy.ToString()),
                new Claim(CustomClaims.Semester, userInfo.Semester.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(AppConfig.JwtIssuer,
              AppConfig.JwtIssuer,
              claims,
              expires: DateTime.Now.AddHours(1),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

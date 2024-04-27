using ItMarathon.Service.Authentication.Dto;

namespace ItMarathon.Service.Authentication
{
    public interface IAuthenticationService
    {
        public Task<string> RegisterAsync(CreateUserDto user);

        public Task<UserDto?> LoginAsync(LoginUserDto login);

        public string GenerateJSONWebToken(UserDto userInfo);
    }
}

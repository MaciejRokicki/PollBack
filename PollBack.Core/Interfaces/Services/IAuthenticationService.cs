using PollBack.Core.Models;

namespace PollBack.Core.Interfaces.Services
{
    public interface IAuthenticationService
    {
        public Task<SignInResponse> SignInAsync(SignInRequest signInRequest);
        public Task SignUpAsync(SignUpRequest signUpRequest);
        public Task<SignInResponse> RefreshTokenAsync(string token, string ipAddress);
        public Task RevokeTokenAsync(string token, string ipAddress);
    }
}

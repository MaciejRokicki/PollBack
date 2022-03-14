//https://jasonwatmore.com/post/2022/01/24/net-6-jwt-authentication-with-refresh-tokens-tutorial-with-example-api

using PollBack.Core.Models;
using PollBack.Core.Entities;
using PollBack.Core.Exceptions;
using PollBack.Core.Interfaces.Repositories;
using PollBack.Core.Interfaces.Services;
using PollBack.Shared.AppSettings;
using Microsoft.Extensions.Options;

namespace PollBack.Core.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly SecuritySettings securitySettings;
        private readonly IUserRepository userRepository;
        private readonly IEncryptionService encryptionService;
        private readonly IJwtTokenService jwtTokenService;

        public AuthenticationService(
            IOptions<SecuritySettings> securitySettings,
            IUserRepository userRepository,
            IEncryptionService encryptionService,
            IJwtTokenService jwtTokenService)
        {
            this.securitySettings = securitySettings.Value;
            this.userRepository = userRepository;
            this.encryptionService = encryptionService;
            this.jwtTokenService = jwtTokenService;
        }

        public async Task<SignInResponse> RefreshTokenAsync(string token, string ipAddress)
        {
            User? user = await userRepository.GetAsync(x => x.RefreshTokens.Any(y => y.Token == token));

            if(user == null)
                throw new UserNotFoundException();

            RefreshToken refreshToken = user.RefreshTokens.Single(x => x.Token == token);

            if(refreshToken.IsRevoked)
            {
                revokeRefreshTokens(refreshToken, user, ipAddress, $"Attempted reuse of revoked ancestor token: {token}");

                //await userRepository.UpdateAsync(user);
            }

            if(!refreshToken.IsActive)
                throw new InactiveTokenException();

            RefreshToken newRefreshToken = await rotateRefreshToken(refreshToken, ipAddress);
            user.RefreshTokens.Add(newRefreshToken);

            removeOldUserRefreshTokens(user);

            await userRepository.UpdateAsync(user);

            string jwtToken = jwtTokenService.GenerateJwtToken(user);

            SignInResponse response = new(user.Id, user.Email, jwtToken, newRefreshToken.Token);

            return response;
        }

        public async Task RevokeTokenAsync(string token, string ipAddress)
        {
            User? user = await userRepository.GetAsync(x => x.RefreshTokens.Any(y => y.Token == token));

            if (user == null)
                throw new UserNotFoundException();

            RefreshToken refreshToken = user.RefreshTokens.Single(x => x.Token == token);

            if (!refreshToken.IsActive)
                throw new InactiveTokenException();

            revokeRefreshToken(refreshToken, ipAddress, "Revoked without replacement");

            await userRepository.UpdateAsync(user);
        }

        public async Task<SignInResponse> SignInAsync(SignInRequest signInRequest)
        {
            User? user = await userRepository.GetAsync(x => x.Email == signInRequest.Email);

            if (user != null)
            {
                if (!encryptionService.VerifySHA256(signInRequest.Password, user.Password))
                {
                    throw new WrongPasswordException();
                }

                string token = jwtTokenService.GenerateJwtToken(user);

                RefreshToken refreshToken = await jwtTokenService.GenerateRefreshToken(signInRequest.IpAddress);

                user.RefreshTokens.Add(refreshToken);
                removeOldUserRefreshTokens(user);

                await userRepository.UpdateAsync(user);

                SignInResponse signInResponse = new(user.Id, user.Email, token, refreshToken.Token);

                return signInResponse;
            }
            else
            {
                throw new UserNotFoundException();
            }
        }

        public async Task SignUpAsync(SignUpRequest signUpRequest)
        {
            User? user = await userRepository.GetAsync(x => x.Email == signUpRequest.Email);

            if (user != null)
            {
                throw new EmailExistsException();
            }

            string hash = encryptionService.EncryptBySHA256(signUpRequest.Password);

            user = new()
            {
                Email = signUpRequest.Email,
                Password = hash
            };

            await userRepository.CreateAsync(user);
        }

        private void removeOldUserRefreshTokens(User user)
        {
            user.RefreshTokens.RemoveAll(x => !x.IsActive && x.Created.AddDays(securitySettings.RefeshTokenDaysLive) <= DateTime.UtcNow);
        }

        private async Task<RefreshToken> rotateRefreshToken(RefreshToken refreshToken, string ipAddress)
        {
            RefreshToken newRefreshToken = await jwtTokenService.GenerateRefreshToken(ipAddress);

            revokeRefreshToken(refreshToken, ipAddress, "Replaced by new token", newRefreshToken.Token);

            return newRefreshToken;
        }

        private void revokeRefreshTokens(RefreshToken refreshToken, User user, string ipAddress, string reason)
        {
            if(!string.IsNullOrEmpty(refreshToken.ReplacedByToken))
            {
                RefreshToken childToken = user.RefreshTokens.Single(x => x.Token == refreshToken.ReplacedByToken);

                if (childToken.IsActive)
                    revokeRefreshToken(childToken, ipAddress, reason);
                else
                    revokeRefreshTokens(childToken, user, ipAddress, reason);

            }
        }

        private void revokeRefreshToken(RefreshToken token, string ipAddres, string reason = "", string replacedByToken = "")
        {
            token.Revoked = DateTime.UtcNow;
            token.RevokedByIp = ipAddres;
            token.ReasonRevoked = reason;
            token.ReplacedByToken = replacedByToken;
        }
    }
}

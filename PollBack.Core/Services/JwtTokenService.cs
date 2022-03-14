using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PollBack.Core.Interfaces.Repositories;
using PollBack.Core.Interfaces.Services;
using PollBack.Shared.AppSettings;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using PollBack.Core.Entities;

namespace PollBack.Core.Services
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly SecuritySettings securitySettings;
        private readonly IUserRepository userRepository;

        public JwtTokenService(IOptions<SecuritySettings> securitySettings, IUserRepository userRepository)
        {
            this.securitySettings = securitySettings.Value;
            this.userRepository = userRepository;
        }

        public string GenerateJwtToken(User user)
        {
            JwtSecurityTokenHandler tokenHandler = new();
            byte[] key = Encoding.ASCII.GetBytes(securitySettings.Secret);
            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public async Task<RefreshToken> GenerateRefreshToken(string ipAddress)
        {
            RefreshToken refreshToken = new(await getUniqueToken(), ipAddress)
            {
                Expires = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow
            };

            return refreshToken;

            async Task<string> getUniqueToken()
            {
                string token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

                User? tokenIsUnique = await userRepository.GetAsync(x => x.RefreshTokens.Any(y => y.Token == token));

                if (tokenIsUnique != null)
                    return await getUniqueToken();

                return token;
            }
        }

        public int? ValidateJwtToken(string token)
        {
            if (token == null)
                return null;

            JwtSecurityTokenHandler tokenHandler = new();
            byte[] key = Encoding.ASCII.GetBytes(securitySettings.Secret);

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                JwtSecurityToken jwtToken = (JwtSecurityToken)validatedToken;
                int userId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);

                return userId;
            }
            catch
            {
                return null;
            }
        }
    }
}

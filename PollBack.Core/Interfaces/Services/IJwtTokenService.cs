using PollBack.Core.UserAggregate;
using PollBack.Core.Entities;

namespace PollBack.Core.Interfaces.Services
{
    public interface IJwtTokenService
    {
        public string GenerateJwtToken(User user);
        public int? ValidateJwtToken(string token);
        public Task<RefreshToken> GenerateRefreshToken(string ipAddress);
    }
}

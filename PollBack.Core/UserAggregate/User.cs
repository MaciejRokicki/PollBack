using PollBack.Core.Entities;
using PollBack.Shared;

namespace PollBack.Core.UserAggregate
{
    public class User : BaseEntity
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public List<RefreshToken> RefreshTokens { get; set; }

        public User(string email, string password)
        {
            Email = email;
            Password = password;
            RefreshTokens = new List<RefreshToken>();
        }
    }
}

using PollBack.Core.PollAggregate;
using PollBack.Shared;

namespace PollBack.Core.Entities
{
    public class User : BaseEntity
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public List<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
        public List<Poll> Polls { get; set; } = new List<Poll>();
        public List<PollOptionVote> PollOptionVotes { get; set; } = new List<PollOptionVote>();
    }
}

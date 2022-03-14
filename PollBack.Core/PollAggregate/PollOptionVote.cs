using PollBack.Core.Entities;
using PollBack.Shared;

namespace PollBack.Core.PollAggregate
{
    public class PollOptionVote : BaseEntity
    {
        public int? UserId { get; set; }
        public User? User { get; set; }
        public int PollOptionId { get; set; }
        public PollOption? PollOption { get; set; }
    }
}

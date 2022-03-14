using MediatR;
using System.Text.Json.Serialization;

namespace PollBack.Core.PollAggregate.Commands
{
    public class VotePollCommand : IRequest
    {
        [JsonIgnore]
        public int? UserId { get; set; }
        public int PollId { get; set; }
        public ICollection<int> PollOptionIds { get; set; } = Array.Empty<int>();
    }
}

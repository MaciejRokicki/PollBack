using MediatR;
using System.Text.Json.Serialization;

namespace PollBack.Core.PollAggregate.Commands
{
    public class ActivatePollCommand : IRequest<Poll>
    {
        [JsonIgnore]
        public int UserId { get; set; }
        public int PollId { get; set; }
    }
}

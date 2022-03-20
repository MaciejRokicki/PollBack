using MediatR;
using System.Text.Json.Serialization;

namespace PollBack.Core.PollAggregate.Commands
{
    public class UpdatePollCommand : IRequest<Poll>
    {
        [JsonIgnore]
        public int UserId { get; set; }
        public Poll Model { get; }

        public UpdatePollCommand(Poll model)
        {
            this.Model = model;
        }
    }
}

using MediatR;
using System.Text.Json.Serialization;

namespace PollBack.Core.PollAggregate.Commands
{
    public class UpdatePollCommand : IRequest<Poll>
    {
        [JsonIgnore]
        public int UserId { get; set; }
        public Poll Model { get; }
        public string? EndOption { get; set; }

        public UpdatePollCommand(Poll model, string endOption)
        {
            this.Model = model;
            this.EndOption = endOption;

            model.End = EndOption == null ? null : model.Created.AddMinutes(EndDateSetter.EndDates[EndOption]);
        }
    }
}

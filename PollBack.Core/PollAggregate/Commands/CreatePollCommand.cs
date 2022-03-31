using MediatR;

namespace PollBack.Core.PollAggregate.Commands
{
    public class CreatePollCommand : IRequest<Poll>
    {
        public Poll Model { get; set; }
        public string? EndOption { get; set; }

        public CreatePollCommand(Poll model, string? endOption)
        {
            this.Model = model;
            this.EndOption = endOption;

            model.End = EndOption == null ? null : model.Created.AddMinutes(EndDateSetter.EndDates[EndOption]);
        }
    }
}

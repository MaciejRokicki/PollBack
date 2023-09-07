using MediatR;

namespace PollBack.Core.PollAggregate.Commands
{
    public class CreatePollCommand : IRequest<Poll>
    {
        public Poll Model { get; set; }
        public string EndOption { get; set; } = string.Empty;

        public CreatePollCommand(Poll model, string endOption)
        {
            Model = model;
            EndOption = endOption;

            model.End = model.Created.AddMinutes(EndDateSetter.EndDates[EndOption]);
        }
    }
}

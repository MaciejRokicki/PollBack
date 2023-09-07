using MediatR;

namespace PollBack.Core.PollAggregate.Commands
{
    public class CreatePollCommand : IRequest<Poll>
    {
        public Poll Model { get; set; }
        public string? EndOption { get; set; }

        public CreatePollCommand(Poll model, string? endOption)
        {
            Model = model;
            EndOption = endOption;
        }
    }
}

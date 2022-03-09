using MediatR;

namespace PollBack.Core.PollAggregate.Commands
{
    public class CreatePollCommand : IRequest<Poll>
    {
        public Poll Model { get; }

        public CreatePollCommand(Poll model)
        {
            this.Model = model;
        }
    }
}

using MediatR;
using PollBack.Core.Interfaces.Repositories;
using PollBack.Core.PollAggregate.Commands;

namespace PollBack.Core.PollAggregate.CommandHandlers
{
    public class CreatePollCommandHandler : IRequestHandler<CreatePollCommand, Poll>
    {
        private readonly IPollRepository pollRepository;

        public CreatePollCommandHandler(IPollRepository pollRepository)
        {
            this.pollRepository = pollRepository;
        }

        public async Task<Poll> Handle(CreatePollCommand request, CancellationToken cancellationToken)
        {
            Poll? data = await pollRepository.CreateAsync(request.Model);

            return data;
        }
    }
}

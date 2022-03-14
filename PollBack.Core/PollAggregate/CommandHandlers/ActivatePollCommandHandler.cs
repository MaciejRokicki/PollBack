using MediatR;
using PollBack.Core.Exceptions;
using PollBack.Core.Interfaces.Repositories;
using PollBack.Core.PollAggregate.Commands;

namespace PollBack.Core.PollAggregate.CommandHandlers
{
    public class ActivatePollCommandHandler : IRequestHandler<ActivatePollCommand, Unit>
    {
        private readonly IPollRepository pollRepository;

        public ActivatePollCommandHandler(IPollRepository pollRepository)
        {
            this.pollRepository = pollRepository;
        }

        public async Task<Unit> Handle(ActivatePollCommand request, CancellationToken cancellationToken)
        {
            Poll? poll = await pollRepository.GetAsync(x => x.Id == request.PollId && x.UserId == request.UserId && x.IsDraft);

            if (poll == null)
                throw new PollNotFoundException();

            poll.IsDraft = false;

            await pollRepository.UpdateAsync(poll);

            return Unit.Value;
        }
    }
}

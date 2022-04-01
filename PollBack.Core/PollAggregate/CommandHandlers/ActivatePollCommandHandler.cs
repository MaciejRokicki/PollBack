using MediatR;
using PollBack.Core.Exceptions;
using PollBack.Core.Interfaces.Repositories;
using PollBack.Core.PollAggregate.Commands;

namespace PollBack.Core.PollAggregate.CommandHandlers
{
    public class ActivatePollCommandHandler : IRequestHandler<ActivatePollCommand, Poll>
    {
        private readonly IPollRepository pollRepository;

        public ActivatePollCommandHandler(IPollRepository pollRepository)
        {
            this.pollRepository = pollRepository;
        }

        public async Task<Poll> Handle(ActivatePollCommand request, CancellationToken cancellationToken)
        {
            Poll? poll = await pollRepository.GetAsync(x => x.Id == request.PollId && x.UserId == request.UserId && x.IsDraft);

            if (poll == null)
                throw new PollNotFoundException();

            if(poll.End != null)
            {
                TimeSpan? x = poll.End - poll.Created;

                poll.Created = DateTime.UtcNow;
                poll.End = poll.Created.Add(x.Value);
            }

            poll.IsDraft = false;

            poll = await pollRepository.UpdateAsync(poll);

            return poll;
        }
    }
}

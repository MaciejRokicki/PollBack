using MediatR;
using PollBack.Core.Interfaces.Repositories;
using PollBack.Core.PollAggregate.Queries;

namespace PollBack.Core.PollAggregate.QueryHandlers
{
    public class GetPollByIdQueryHandler : IRequestHandler<GetPollByIdQuery, Poll?>
    {
        private readonly IPollRepository pollRepository;

        public GetPollByIdQueryHandler(IPollRepository pollRepository)
        {
            this.pollRepository = pollRepository;
        }

        public async Task<Poll?> Handle(GetPollByIdQuery request, CancellationToken cancellationToken)
        {
            Poll? poll = await pollRepository.GetAsync(x => x.Id == request.PollId);

            return poll;
        }
    }
}

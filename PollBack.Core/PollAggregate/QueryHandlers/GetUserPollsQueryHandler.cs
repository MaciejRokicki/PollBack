using MediatR;
using PollBack.Core.Interfaces.Repositories;
using PollBack.Core.PollAggregate.Queries;

namespace PollBack.Core.PollAggregate.QueryHandlers
{
    public class GetUserPollsQueryHandler : IRequestHandler<GetUserPollsQuery, IEnumerable<Poll>>
    {
        private readonly IPollRepository pollRepository;

        public GetUserPollsQueryHandler(IPollRepository pollRepository)
        {
            this.pollRepository = pollRepository;
        }

        public async Task<IEnumerable<Poll>> Handle(GetUserPollsQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Poll> polls = await pollRepository.GetManyAsync(x => x.UserId == request.UserId);

            return polls;
        }
    }
}

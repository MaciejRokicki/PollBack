using MediatR;
using PollBack.Core.Interfaces.Repositories;
using PollBack.Core.PollAggregate.Queries;

namespace PollBack.Core.PollAggregate.QueryHandlers
{
    public class GetAllPollsQueryHandler : IRequestHandler<GetAllPollsQuery, IEnumerable<Poll>>
    {
        private readonly IPollRepository pollRepository;

        public GetAllPollsQueryHandler(IPollRepository pollRepository)
        {
            this.pollRepository = pollRepository;
        }

        public async Task<IEnumerable<Poll>> Handle(GetAllPollsQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Poll>? data = await pollRepository.GetManyAsync();

            return data;
        }
    }
}

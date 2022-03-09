using MediatR;

namespace PollBack.Core.PollAggregate.Queries
{
    public class GetAllPollsQuery : IRequest<IEnumerable<Poll>>
    {

    }
}

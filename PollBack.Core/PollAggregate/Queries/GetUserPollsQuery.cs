using MediatR;

namespace PollBack.Core.PollAggregate.Queries
{
    public record GetUserPollsQuery(int UserId) : IRequest<IEnumerable<Poll>>;
}

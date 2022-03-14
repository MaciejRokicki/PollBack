using MediatR;

namespace PollBack.Core.PollAggregate.Queries
{
    public class GetUserPollsQuery : IRequest<IEnumerable<Poll>>
    {
        public int UserId { get; set; }

        public GetUserPollsQuery(int userId)
        {
            UserId = userId;
        }
    }
}

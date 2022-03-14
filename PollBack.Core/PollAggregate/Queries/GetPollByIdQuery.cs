using MediatR;

namespace PollBack.Core.PollAggregate.Queries
{
    public class GetPollByIdQuery : IRequest<Poll>
    {
        public int PollId { get; set; }

        public GetPollByIdQuery(int pollId)
        {
            PollId = pollId;
        }
    }
}

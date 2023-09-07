using MediatR;

namespace PollBack.Core.PollAggregate.Queries
{
    public record GetPollByIdQuery(int PollId) : IRequest<Poll>;
}

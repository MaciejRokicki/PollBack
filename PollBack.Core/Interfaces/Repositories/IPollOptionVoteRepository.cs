using PollBack.Core.PollAggregate;
using PollBack.Shared.Data;

namespace PollBack.Core.Interfaces.Repositories
{
    public interface IPollOptionVoteRepository : IRepositoryBase<PollOptionVote>
    {
        public Task CreateRangeAsync(IEnumerable<PollOptionVote> pollOptionVotes);
    }
}

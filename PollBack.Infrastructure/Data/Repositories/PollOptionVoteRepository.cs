using PollBack.Core.Interfaces.Repositories;
using PollBack.Core.PollAggregate;
using PollBack.Shared.Data;

namespace PollBack.Infrastructure.Data.Repositories
{
    public class PollOptionVoteRepository : RepositoryBase<PollOptionVote>, IPollOptionVoteRepository
    {
        private readonly AppDbContext appDbContext;

        public PollOptionVoteRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task CreateRangeAsync(IEnumerable<PollOptionVote> pollOptionVotes)
        {
            await appDbContext
                .Set<PollOptionVote>()
                .AddRangeAsync(pollOptionVotes);

            await appDbContext.SaveChangesAsync();
        }
    }
}

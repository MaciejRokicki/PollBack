using PollBack.Core.PollAggregate;
using PollBack.Infrastructure.Data.Interfaces;
using PollBack.Shared.Data;

namespace PollBack.Infrastructure.Data.Repositories
{
    public class PollRepository : RepositoryBase<Poll>, IPollRepository
    {
        private readonly AppDbContext appDbContext;

        public PollRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            this.appDbContext = appDbContext;
        }
    }
}

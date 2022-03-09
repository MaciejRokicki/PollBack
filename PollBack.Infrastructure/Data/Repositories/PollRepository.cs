using PollBack.Core.Interfaces.Repositories;
using PollBack.Core.PollAggregate;
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

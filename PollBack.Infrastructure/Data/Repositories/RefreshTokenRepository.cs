using PollBack.Core.Entities;
using PollBack.Core.Interfaces.Repositories;
using PollBack.Shared.Data;

namespace PollBack.Infrastructure.Data.Repositories
{
    public class RefreshTokenRepository : RepositoryBase<RefreshToken>, IRefreshTokenRepository
    {
        private readonly AppDbContext appDbContext;

        public RefreshTokenRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            this.appDbContext = appDbContext;
        }
    }
}

using PollBack.Core.UserAggregate;
using PollBack.Core.Interfaces.Repositories;
using PollBack.Shared.Data;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace PollBack.Infrastructure.Data.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        private readonly AppDbContext appDbContext;

        public UserRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public override async Task<User?> GetAsync(Expression<Func<User, bool>> expression) => await appDbContext.Set<User>().Include(x => x.RefreshTokens).SingleOrDefaultAsync(expression);
    }
}

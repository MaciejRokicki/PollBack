using Microsoft.EntityFrameworkCore;
using PollBack.Core.Interfaces.Repositories;
using PollBack.Core.PollAggregate;
using PollBack.Shared.Data;
using System.Linq.Expressions;

namespace PollBack.Infrastructure.Data.Repositories
{
    public class PollRepository : RepositoryBase<Poll>, IPollRepository
    {
        private readonly AppDbContext appDbContext;

        public PollRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public override async Task<Poll?> GetAsync(Expression<Func<Poll, bool>> expression)
            => await appDbContext
                .Set<Poll>()
                .AsNoTracking()
                .Select(x => new Poll()
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    User = x.User,
                    Question = x.Question,
                    IsDraft = x.IsDraft,
                    Created = x.Created,
                    End = x.End,
                    Options = x.Options
                        .Select(y => new PollOption()
                        {
                            Id = y.Id,
                            PollId = y.PollId,
                            Poll = y.Poll,
                            Option = y.Option,
                            Votes = y.PollOptionVotes.Count(),
                            PollOptionVotes = y.PollOptionVotes
                        })
                        .ToList()
                })
                .FirstOrDefaultAsync(expression);

        public override async Task<IEnumerable<Poll>> GetManyAsync(Expression<Func<Poll, bool>> expression)
            => await appDbContext
                .Set<Poll>()
                .AsNoTracking()
                .Select(x => new Poll()
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    User = x.User,
                    Question = x.Question,
                    IsDraft = x.IsDraft,
                    Created = x.Created,
                    End = x.End,
                    Options = x.Options
                        .Select(y => new PollOption()
                        {
                            Id = y.Id,
                            PollId = y.PollId,
                            Poll = y.Poll,
                            Votes = y.PollOptionVotes.Count(),
                            PollOptionVotes = y.PollOptionVotes
                        })
                        .ToList()
                })
                .Where(expression)
                .ToListAsync();
    }
}

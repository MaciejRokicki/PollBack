using Microsoft.EntityFrameworkCore;
using PollBack.Core.Entities;
using PollBack.Core.PollAggregate;
using PollBack.Infrastructure.Data.Configurations;

namespace PollBack.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Poll> Polls => Set<Poll>();
        public DbSet<PollOption> Options => Set<PollOption>();
        public DbSet<PollOptionVote> OptionVotes => Set<PollOptionVote>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RefreshTokenConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguartion());
            modelBuilder.ApplyConfiguration(new PollConfiguration());
            modelBuilder.ApplyConfiguration(new PollOptionConfiguration());
            modelBuilder.ApplyConfiguration(new PollOptionVoteConfiguration());
        }
    }
}

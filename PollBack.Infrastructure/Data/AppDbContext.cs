using Microsoft.EntityFrameworkCore;
using PollBack.Core.PollAggregate;
using PollBack.Infrastructure.Data.Configurations;

namespace PollBack.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Poll> Polls => Set<Poll>();
        public DbSet<PollOption> Options => Set<PollOption>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PollConfiguration());
            modelBuilder.ApplyConfiguration(new PollOptionConfiguration());
        }
    }
}

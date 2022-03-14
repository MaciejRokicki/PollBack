using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PollBack.Core.PollAggregate;

namespace PollBack.Infrastructure.Data.Configurations
{
    public class PollOptionVoteConfiguration : IEntityTypeConfiguration<PollOptionVote>
    {
        public void Configure(EntityTypeBuilder<PollOptionVote> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .HasOne(x => x.User)
                .WithMany(x => x.PollOptionVotes)
                .HasForeignKey(x => x.UserId);

            builder
                .HasOne(x => x.PollOption)
                .WithMany(x => x.PollOptionVotes)
                .HasForeignKey(x => x.PollOptionId);

            builder
                .ToTable("PollOptionVotes");
        }
    }
}

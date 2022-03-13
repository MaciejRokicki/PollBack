using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PollBack.Core.PollAggregate;

namespace PollBack.Infrastructure.Data.Configurations
{
    public class PollConfiguration : IEntityTypeConfiguration<Poll>
    {
        public void Configure(EntityTypeBuilder<Poll> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.IsDraft)
                .IsRequired();

            builder
                .Property(x => x.Created)
                .IsRequired();

            builder
                .Property(x => x.End)
                .IsRequired();

            builder
                .Property(x => x.Question)
                .IsRequired();

            builder
                .HasMany(x => x.Options)
                .WithOne(x => x.Poll)
                .HasForeignKey(x => x.PollId);

            builder
                .ToTable("Polls");
        }
    }
}

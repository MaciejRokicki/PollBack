using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PollBack.Core.PollAggregate;

namespace PollBack.Infrastructure.Data.Configurations
{
    public class PollOptionConfiguration : IEntityTypeConfiguration<PollOption>
    {
        public void Configure(EntityTypeBuilder<PollOption> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Option)
                .IsRequired();

            builder
                .Property(x => x.Votes)
                .HasDefaultValue(0);

            builder
                .ToTable("PollOptions");
        }
    }
}

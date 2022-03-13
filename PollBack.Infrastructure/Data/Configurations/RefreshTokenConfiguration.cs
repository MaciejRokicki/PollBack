using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PollBack.Core.Entities;

namespace PollBack.Infrastructure.Data.Configurations
{
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Expires)
                .IsRequired();

            builder
                .Property(x => x.Created)
                .HasDefaultValue(DateTime.UtcNow)
                .IsRequired();

            builder
                .Property(x => x.CreatedByIp)
                .IsRequired();

            builder
                .Property(x => x.Revoked);

            builder
                .Property(x => x.RevokedByIp);

            builder
                .Property(x => x.ReplacedByToken);

            builder
                .Property(x => x.ReasonRevoked);

            builder
                .ToTable("RefreshTokens");
        }
    }
}

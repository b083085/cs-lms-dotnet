using Capstone.LMS.Domain.Constants;
using Capstone.LMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Capstone.LMS.Persistence.Configurations
{
    internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(p => p.FirstName).IsRequired().HasMaxLength(50);
            builder.Property(p => p.LastName).IsRequired().HasMaxLength(50);
            builder.Property(p => p.Gender).IsRequired().HasMaxLength(1);

            builder.Property(p => p.CreatedBy).IsRequired();
            builder.Property(p => p.ModifiedBy).IsRequired();
            builder.Property(p => p.CreatedAtUtc).IsRequired();
            builder.Property(p => p.ModifiedAtUtc).IsRequired();

            builder.HasQueryFilter(p => p.DeletedAtUtc == null);

            builder.ToTable(EntityTables.Users);
        }
    }
}

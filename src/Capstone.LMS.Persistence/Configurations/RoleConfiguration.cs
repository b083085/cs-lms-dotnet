using Capstone.LMS.Domain.Constants;
using Capstone.LMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Capstone.LMS.Persistence.Configurations
{
    internal sealed class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.Property(p => p.CreatedBy).IsRequired();
            builder.Property(p => p.ModifiedBy).IsRequired();
            builder.Property(p => p.CreatedAtUtc).IsRequired();
            builder.Property(p => p.ModifiedAtUtc).IsRequired();

            builder.HasQueryFilter(p => p.DeletedAtUtc == null);

            builder.ToTable(EntityTables.Roles);
        }
    }
}

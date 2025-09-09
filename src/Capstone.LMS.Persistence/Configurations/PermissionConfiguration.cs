using Capstone.LMS.Domain.Constants;
using Capstone.LMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Capstone.LMS.Persistence.Configurations
{
    internal sealed class PermissionConfiguration 
        : BaseEntityConfiguration<Permission>, IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            ConfigureDefault(builder);

            builder.Property(p => p.PermissionName).IsRequired().HasMaxLength(500);
            builder.Property(p => p.Enabled).IsRequired();
            builder.Property(p => p.BitwiseValue).IsRequired();

            builder.HasIndex(p => p.PermissionName).IsUnique();

            builder.ToTable(EntityTables.Permissions);
        }
    }
}

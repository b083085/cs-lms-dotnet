using Capstone.LMS.Domain.Constants;
using Capstone.LMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Capstone.LMS.Persistence.Configurations
{
    internal sealed class SubPermissionConfiguration 
        : BaseEntityConfiguration<SubPermission>, IEntityTypeConfiguration<SubPermission>
    {
        public void Configure(EntityTypeBuilder<SubPermission> builder)
        {
            ConfigureDefault(builder);

            builder.Property(p => p.SubPermissionName).IsRequired().HasMaxLength(500);
            builder.Property(p => p.Enabled).IsRequired();
            builder.Property(p => p.BitwiseValue).IsRequired();

            builder.HasIndex(p => p.SubPermissionName).IsUnique();

            builder.ToTable(EntityTables.SubPermissions);
        }
    }
}

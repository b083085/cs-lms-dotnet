using Capstone.LMS.Domain.Constants;
using Capstone.LMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Capstone.LMS.Persistence.Configurations
{
    internal sealed class AccessControlConfiguration 
        : BaseEntityConfiguration<AccessControl>, IEntityTypeConfiguration<AccessControl>
    {
        public void Configure(EntityTypeBuilder<AccessControl> builder)
        {
            ConfigureDefault(builder);

            builder.Property(p => p.RoleId).IsRequired();
            builder.Property(p => p.PermissionId).IsRequired();
            builder.Property(p => p.SubPermissionId).IsRequired();

            builder.HasIndex(p => p.RoleId);

            builder.HasOne(p => p.Role)
                .WithMany(p => p.AccessControls)
                .HasForeignKey(p => p.RoleId);

            builder.HasOne(p => p.Permission)
                .WithMany(p => p.AccessControls)
                .HasForeignKey(p => p.PermissionId);

            builder.HasOne(p => p.SubPermission)
                .WithMany(p => p.AccessControls)
                .HasForeignKey(p => p.SubPermissionId);

            builder.ToTable(EntityTables.AccessControls);
        }
    }
}

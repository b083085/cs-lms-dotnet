using Capstone.LMS.Domain.Constants;
using Capstone.LMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Capstone.LMS.Persistence.Configurations
{
    internal sealed class DefaultPermissionConfiguration 
        : BaseEntityConfiguration<DefaultPermission>, IEntityTypeConfiguration<DefaultPermission>
    {
        public void Configure(EntityTypeBuilder<DefaultPermission> builder)
        {
            ConfigureDefault(builder);

            builder.Property(p => p.PermissionId).IsRequired();
            builder.Property(p => p.SubPermissionId).IsRequired();

            builder.HasIndex(p => p.PermissionId);
            builder.HasIndex(p => p.SubPermissionId);

            builder.HasOne(p => p.Permission)
                .WithMany(p => p.DefaultPermissions)
                .HasForeignKey(p => p.PermissionId);

            builder.HasOne(p => p.SubPermission)
                .WithMany(p => p.DefaultPermissions)
                .HasForeignKey(p => p.SubPermissionId);
                
            builder.ToTable(EntityTables.DefaultPermissions);
        }
    }
}

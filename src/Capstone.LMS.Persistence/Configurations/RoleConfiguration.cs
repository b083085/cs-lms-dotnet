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
            builder.Property(p => p.CreatedBy).IsRequired().HasColumnOrder(100);
            builder.Property(p => p.CreatedOnUtc).IsRequired().HasColumnOrder(101);
            builder.Property(p => p.ModifiedBy).IsRequired().HasColumnOrder(102);
            builder.Property(p => p.ModifiedOnUtc).IsRequired().HasColumnOrder(103);
            builder.Property(p => p.DeletedBy).HasColumnOrder(104);
            builder.Property(p => p.DeletedOnUtc).HasColumnOrder(105);

            builder.HasQueryFilter(p => p.DeletedOnUtc == null);

            builder.ToTable(EntityTables.Roles);
        }
    }
}

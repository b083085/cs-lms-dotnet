using Capstone.LMS.Domain.Primitives;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Capstone.LMS.Persistence.Configurations
{
    public abstract class BaseEntityConfiguration<T> where T : Entity
    {
        protected void ConfigureDefault(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.PublicId).IsRequired();
            builder.Property(p => p.CreatedBy).IsRequired().HasColumnOrder(100);
            builder.Property(p => p.CreatedOnUtc).IsRequired().HasColumnOrder(101);
            builder.Property(p => p.ModifiedBy).IsRequired().HasColumnOrder(102);
            builder.Property(p => p.ModifiedOnUtc).IsRequired().HasColumnOrder(103);
            builder.Property(p => p.DeletedBy).HasColumnOrder(104);
            builder.Property(p => p.DeletedOnUtc).HasColumnOrder(105);

            builder.HasQueryFilter(p => p.DeletedOnUtc == null);
        }
    }
}

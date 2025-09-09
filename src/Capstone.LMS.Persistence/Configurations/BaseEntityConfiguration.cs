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

            builder.Property(p => p.CreatedBy).IsRequired();
            builder.Property(p => p.ModifiedBy).IsRequired();
            builder.Property(p => p.CreatedAtUtc).IsRequired();
            builder.Property(p => p.ModifiedAtUtc).IsRequired();

            builder.HasQueryFilter(p => p.DeletedAtUtc == null);
        }
    }
}

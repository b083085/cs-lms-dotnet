using Capstone.LMS.Domain.Constants;
using Capstone.LMS.Domain.Entities;
using Capstone.LMS.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Capstone.LMS.Persistence.Configurations
{
    internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(p => p.PublicId).IsRequired();
            builder.Property(p => p.FirstName).IsRequired().HasMaxLength(50);
            builder.Property(p => p.LastName).IsRequired().HasMaxLength(50);
            
            builder
                .Property(p => p.Gender)
                .IsRequired()
                .HasMaxLength(1)
                .HasConversion(
                gender => gender.Value,
                value => Gender.Create(value).Value);

            builder.Property(p => p.CreatedBy).IsRequired().HasColumnOrder(100);
            builder.Property(p => p.CreatedOnUtc).IsRequired().HasColumnOrder(101);
            builder.Property(p => p.ModifiedBy).IsRequired().HasColumnOrder(102);
            builder.Property(p => p.ModifiedOnUtc).IsRequired().HasColumnOrder(103);
            builder.Property(p => p.DeletedBy).HasColumnOrder(104);
            builder.Property(p => p.DeletedOnUtc).HasColumnOrder(105);

            builder.HasQueryFilter(p => p.DeletedOnUtc == null);

            builder.ToTable(EntityTables.Users);
        }
    }
}

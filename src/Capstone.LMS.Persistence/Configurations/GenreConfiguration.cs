using Capstone.LMS.Domain.Constants;
using Capstone.LMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Capstone.LMS.Persistence.Configurations
{
    internal sealed class GenreConfiguration : BaseEntityConfiguration<Genre>, IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            ConfigureDefault(builder);

            builder.Property(p => p.Name).IsRequired().HasMaxLength(100);

            builder.HasIndex(p => p.Name).IsUnique();
            
            builder.ToTable(EntityTables.Genres);
        }
    }
}

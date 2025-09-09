using Capstone.LMS.Domain.Constants;
using Capstone.LMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Capstone.LMS.Persistence.Configurations
{
    internal sealed class BookConfiguration 
        : BaseEntityConfiguration<Book>, IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            ConfigureDefault(builder);

            builder.Property(p => p.Title).IsRequired().HasMaxLength(1000);
            builder.Property(p => p.Summary).IsRequired();
            builder.Property(p => p.PublishedOn).IsRequired();
            builder.Property(p => p.GenreId).IsRequired();
            builder.Property(p => p.TotalCopies).IsRequired();
            builder.Property(p => p.AuthorId).IsRequired();

            builder.HasIndex(p => p.Title).IsUnique();
            builder.HasIndex(p => p.PublishedOn);
            builder.HasIndex(p => p.GenreId);
            builder.HasIndex(p => p.AuthorId);

            builder.HasOne(p => p.Author)
                .WithMany(p => p.Books)
                .HasForeignKey(p => p.AuthorId);
          
            builder.HasOne(p => p.Genre)
                .WithMany(p => p.Books)
                .HasForeignKey(p => p.GenreId);

            builder.ToTable(EntityTables.Books);
        }
    }
}

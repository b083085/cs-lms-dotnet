using Capstone.LMS.Domain.Constants;
using Capstone.LMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Capstone.LMS.Persistence.Configurations
{
    internal sealed class BorrowedBookConfiguration 
        : BaseEntityConfiguration<BorrowedBook>, IEntityTypeConfiguration<BorrowedBook>
    {
        public void Configure(EntityTypeBuilder<BorrowedBook> builder)
        {
            ConfigureDefault(builder);

            builder.Property(p => p.BookId).IsRequired();
            builder.Property(p => p.UserId).IsRequired();
            builder.Property(p => p.Status).IsRequired();

            builder.HasIndex(p => p.BookId);
            builder.HasIndex(p => p.UserId);
            builder.HasIndex(p => p.Status);

            builder.HasOne(p => p.Book)
                .WithMany(p => p.BorrowedBooks)
                .HasForeignKey(p => p.BookId);

            builder.HasOne(p => p.User)
                .WithMany(p => p.BorrowedBooks)
                .HasForeignKey(p => p.UserId);

            builder.HasOne(p => p.Approver)
                .WithMany(p => p.ApproverBorrowedBooks)
                .HasForeignKey(p => p.ApprovedBy)
                .OnDelete(DeleteBehavior.NoAction);

            builder.ToTable(EntityTables.BorrowedBooks);
        }
    }
}

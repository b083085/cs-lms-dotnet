using Capstone.LMS.Domain.Constants;
using Capstone.LMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Capstone.LMS.Persistence.Configurations
{
    internal sealed class OutboxMessageConfiguration : IEntityTypeConfiguration<OutboxMessage>
    {
        public void Configure(EntityTypeBuilder<OutboxMessage> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Type).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Content).IsRequired(); 

            builder.ToTable(EntityTables.OutboxMessages);
        }
    }
}

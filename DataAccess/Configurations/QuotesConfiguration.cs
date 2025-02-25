using DataAccess.EntityModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations
{
    public class QuotesConfiguration : IEntityTypeConfiguration<QuoteEntity>
    {
        public void Configure(EntityTypeBuilder<QuoteEntity> builder)
        {
            builder.HasKey(q => q.Id);

            builder.Property(q => q.Text)
                .IsRequired()
                .HasMaxLength(250);
        }
    }
}

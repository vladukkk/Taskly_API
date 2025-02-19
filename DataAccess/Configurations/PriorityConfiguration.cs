using DataAccess.EntityModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations
{
    public class PriorityConfiguration : IEntityTypeConfiguration<PriorityEntity>
    {
        public void Configure(EntityTypeBuilder<PriorityEntity> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Title)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.ColorHash)
                .IsRequired()
                .HasMaxLength(7);

            builder.HasMany(p => p.Tasks)
                .WithOne(t => t.Priority)
                .HasForeignKey(t => t.PriorityId);
        }
    }
}

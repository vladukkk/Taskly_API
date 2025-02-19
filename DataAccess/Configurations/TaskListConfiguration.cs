using DataAccess.EntityModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations
{
    public class TaskListConfiguration : IEntityTypeConfiguration<TaskListEntity>
    {
        public void Configure(EntityTypeBuilder<TaskListEntity> builder)
        {
            builder.HasKey(tl => tl.Id);

            builder.Property(tl => tl.Title)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(tl => tl.CreatedAt)
                .IsRequired();

            builder.Property(tl => tl.UpdatedAt)
                .IsRequired();

            builder.HasMany(tl => tl.Tasks)
                .WithOne(t => t.TaskList)
                .HasForeignKey(t => t.TaskListId);
        }
    }
}

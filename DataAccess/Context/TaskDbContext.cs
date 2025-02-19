using DataAccess.Configurations;
using DataAccess.EntityModels;
using DataAccess.EntityModels.ManyToMany;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Context
{
    public class TaskDbContext : DbContext
    {
        public DbSet<TaskListEntity> TaskLists { get; set; }
        public DbSet<TaskEntity> Tasks { get; set; }
        public DbSet<TagEntity> Tags { get; set; }
        public DbSet<PriorityEntity> Priorities { get; set; }
        public DbSet<TaskTagEntity> TaskTags { get; set; }

        public TaskDbContext(DbContextOptions<TaskDbContext> options) :base(options) { }
        public TaskDbContext() { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TaskListConfiguration());
            modelBuilder.ApplyConfiguration(new TaskConfiguration());
            modelBuilder.ApplyConfiguration(new TagConfiguration());
            modelBuilder.ApplyConfiguration(new PriorityConfiguration());
            modelBuilder.ApplyConfiguration(new TaskTagsConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Test_db;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }
    }
}

using DataAccess.Configurations;
using DataAccess.EntityModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Context
{
    public class TaskDbContext : IdentityDbContext<UserEntity>
    {
        private readonly IConfiguration _configuration;

        public DbSet<TaskEntity> Tasks { get; set; }
        public DbSet<TagEntity> Tags { get; set; }
        public DbSet<PriorityEntity> Priorities { get; set; }
        public DbSet<QuoteEntity> Quotes { get; set; }

        public TaskDbContext(DbContextOptions<TaskDbContext> options , IConfiguration configuration) :base(options) 
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TaskConfiguration());
            modelBuilder.ApplyConfiguration(new TagConfiguration());
            modelBuilder.ApplyConfiguration(new PriorityConfiguration());
            modelBuilder.ApplyConfiguration(new QuotesConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}

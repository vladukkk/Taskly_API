using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Context
{
    public class TaskDbContextFactory : IDesignTimeDbContextFactory<TaskDbContext>
    {
        public TaskDbContext CreateDbContext(string[] args)
        {

            var basePath = Path.Combine(Directory.GetCurrentDirectory(), "..", "Taskly_API");
            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<TaskDbContext>();
            var connString = configuration.GetConnectionString("testConn");

            if (string.IsNullOrEmpty(connString))
                throw new InvalidOperationException("conn string is not found");

            optionsBuilder.UseSqlServer(connString);

            return new TaskDbContext(optionsBuilder.Options, configuration);
        }
    }
}

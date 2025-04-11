using DataAccess.Context;
using DataAccess.Contracts;
using DataAccess.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            //config context
            services.AddDbContext<TaskDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("testConn"));
            });

            //repository
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            return services;
        }
    }
}

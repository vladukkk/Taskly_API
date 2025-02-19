using BusinessLogic.Contracts;
using BusinessLogic.Helpers;
using BusinessLogic.Services;
using BusinessLogic.Validators.Priority;
using DataAccess.Context;
using DataAccess.Contracts;
using DataAccess.Repository;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace Taskly_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //add AutoMapper
            builder.Services.AddAutoMapper(typeof(MapperProfile));

            //config context
            builder.Services.AddDbContext<TaskDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("testConn"));
            });

            //repository
            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            //services
            builder.Services.AddScoped<IPriorityService, PriorityService>();
            builder.Services.AddScoped<ITagService, TagService>();

            //validators
            builder.Services.AddFluentValidationAutoValidation()
                            .AddValidatorsFromAssemblyContaining<PriorityAddValidator>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            /*using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<TaskDbContext>();
                context.Database.Migrate();  // Застосовує міграції
                DbInitializer.Seed(context); // Заповнює базу
            }*/

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}

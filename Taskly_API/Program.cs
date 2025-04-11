using BusinessLogic.Helpers;
using DataAccess.Context;
using DataAccess.EntityModels;
using Microsoft.AspNetCore.Identity;
using BusinessLogic;
using DataAccess;

namespace Taskly_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //add AutoMapper
            builder.Services.AddAutoMapper(typeof(MapperProfile));

            builder.Services
                .AddInfrastructure(builder.Configuration)
                .AddBusinessLogic();


            builder.Services.Configure<AuthSettings>(
                builder.Configuration.GetSection("AuthSettings"));

            builder.Services.AddIdentity<UserEntity, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;

                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = false;
            })
            .AddEntityFrameworkStores<TaskDbContext>()
            .AddDefaultTokenProviders();


            string allowPolicy = "AllowFrontend";
            //cors
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(allowPolicy, policy =>
                {
                    policy.WithOrigins("http://localhost:5173");
                    policy.AllowAnyHeader();
                    policy.AllowAnyMethod();
                    policy.AllowCredentials();
                });
            });

            builder.Services.AddAuth(builder.Configuration);


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

            /* using (var scope = app.Services.CreateScope())
             {
                 var context = scope.ServiceProvider.GetRequiredService<TaskDbContext>();
                 context.Database.Migrate();  // Застосовує міграції
                 DbInitializer.Seed(context); // Заповнює базу
             }*/
            app.UseCors(allowPolicy);

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}

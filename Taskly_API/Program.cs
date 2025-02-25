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
using DataAccess.EntityModels;
using Microsoft.AspNetCore.Identity;
using BusinessLogic;

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
            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<IUsersService, UsersService>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<ITaskService, TaskService>();
            builder.Services.AddScoped<IPriorityService, PriorityService>();
            builder.Services.AddScoped<ITagService, TagService>();
            builder.Services.AddScoped<IQuotesService, QuotesService>();
            builder.Services.AddScoped<JwtService>();

            builder.Services.Configure<AuthSettings>(
                builder.Configuration.GetSection("AuthSettings"));

            //validators
            builder.Services.AddFluentValidationAutoValidation()
                            .AddValidatorsFromAssemblyContaining<PriorityAddValidator>();

            //identity
            /*builder.Services.AddIdentity<UserEntity, IdentityRole>()
                .AddEntityFrameworkStores<TaskDbContext>()
                .AddDefaultTokenProviders();*/

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

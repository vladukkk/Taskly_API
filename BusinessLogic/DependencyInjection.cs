using BusinessLogic.Contracts;
using BusinessLogic.Services;
using BusinessLogic.Validators.Priority;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessLogic
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBusinessLogic(this IServiceCollection services)
        {

            //services
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITaskService, TaskService>();
            services.AddScoped<IPriorityService, PriorityService>();
            services.AddScoped<ITagService, TagService>();
            services.AddScoped<IQuotesService, QuotesService>();
            services.AddScoped<JwtService>();

            //validators
            services.AddFluentValidationAutoValidation()
                            .AddValidatorsFromAssemblyContaining<PriorityAddValidator>();

            return services;
        }
    }
}

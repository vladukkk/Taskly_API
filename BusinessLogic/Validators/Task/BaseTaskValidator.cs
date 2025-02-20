using BusinessLogic.Contracts;
using FluentValidation;

namespace BusinessLogic.Validators.Task
{
    public abstract class BaseTaskValidator<T> : AbstractValidator<T> where T : class , ITask
    {
        public BaseTaskValidator()
        {
            RuleFor(t => t.Title)
                .NotEmpty().WithMessage("title can't be empty")
                .MaximumLength(50).WithMessage("Title must be less than 50 characters.");

            RuleFor(t => t.Description)
                .MaximumLength(350).WithMessage("Description must be less than 350 characters.");

            RuleFor(t => t.DueDate)
                .Must(date => date == null || date > DateTime.UtcNow)
                .WithMessage("Due date must be in the future.");
        }
    }
}

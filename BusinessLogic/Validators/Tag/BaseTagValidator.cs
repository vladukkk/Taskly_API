using BusinessLogic.Contracts;
using FluentValidation;

namespace BusinessLogic.Validators.Tag
{
    public abstract class BaseTagValidator<T> : AbstractValidator<T> where T : class, ITag 
    {
        public BaseTagValidator()
        {
             RuleFor(p => p.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(50).WithMessage("Title must be less than 50 characters.");

            RuleFor(p => p.ColorHash)
                    .Matches(@"^#(?:[0-9a-fA-F]{3}){1,2}$")
                    .WithMessage("Invalid format. Use #RRGGBB or #RGB.");
        }
    }
}

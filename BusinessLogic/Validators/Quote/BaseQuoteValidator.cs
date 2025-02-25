using BusinessLogic.Contracts;
using FluentValidation;

namespace BusinessLogic.Validators.Quote
{
    public abstract class BaseQuoteValidator<T> : AbstractValidator<T> where T : class , IQuote
    {
        public BaseQuoteValidator()
        {
            RuleFor(q => q.Text)
                .NotEmpty().WithMessage("Quote can't be empty");
        }
    }
}

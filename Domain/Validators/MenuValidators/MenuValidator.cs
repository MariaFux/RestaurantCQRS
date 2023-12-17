using Domain.Aggregates.MenuAggregate;
using FluentValidation;

namespace Domain.Validators.MenuValidators
{
    public class MenuValidator : AbstractValidator<Menu>
    {
        public MenuValidator()
        {
            RuleFor(r => r.Name)
                .NotNull().WithMessage("Menu name should not be null")
                .NotEmpty().WithMessage("Menu name should not be empty")
                .MaximumLength(50)
                .MinimumLength(3);
        }
    }
}

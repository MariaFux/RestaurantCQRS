using Domain.Aggregates.RecipeAggregate;
using FluentValidation;

namespace Domain.Validators.RecipeValidators
{
    public class RecipeValidator : AbstractValidator<Recipe>
    {
        public RecipeValidator()
        {
            RuleFor(r => r.Name)
                .NotNull().WithMessage("Recipe name should not be null")
                .NotEmpty().WithMessage("Recipe name should not be empty")
                .MaximumLength(50)
                .MinimumLength(3);

            RuleFor(r => r.TextContent)
                .NotNull().WithMessage("Recipe text should not be null")
                .NotEmpty().WithMessage("Recipe text should not be empty")
                .MaximumLength(1000)
                .MinimumLength(1);
        }
    }
}

using Domain.Aggregates.RecipeAggregate;
using FluentValidation;

namespace Domain.Validators.RecipeValidators
{
    internal class RecipeIngredientValidator : AbstractValidator<RecipeIngredient>
    {
        public RecipeIngredientValidator()
        {
            RuleFor(ri => ri.Name)
                .NotNull().WithMessage("Ingredient name should not be null")
                .NotEmpty().WithMessage("Ingredient name should not be empty")
                .MaximumLength(20)
                .MinimumLength(3);
        }
    }
}

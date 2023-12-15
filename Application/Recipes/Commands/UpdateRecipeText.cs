using Application.Models;
using Domain.Aggregates.RecipeAggregate;
using MediatR;

namespace Application.Recipes.Commands
{
    public class UpdateRecipeText : IRequest<OperationResult<Recipe>>
    {
        public string NewText { get; set; }
        public Guid RecipeId { get; set; }
    }
}

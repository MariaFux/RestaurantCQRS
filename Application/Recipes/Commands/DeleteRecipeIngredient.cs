using Application.Models;
using Domain.Aggregates.RecipeAggregate;
using MediatR;

namespace Application.Recipes.Commands
{
    public class DeleteRecipeIngredient : IRequest<OperationResult<RecipeIngredient>>
    {
        public Guid RecipeId { get; set; }
        public Guid IngredientId { get; set; }
    }
}

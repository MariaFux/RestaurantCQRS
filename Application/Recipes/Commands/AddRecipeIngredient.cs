using Application.Models;
using Domain.Aggregates.RecipeAggregate;
using MediatR;

namespace Application.Recipes.Commands
{
    public class AddRecipeIngredient : IRequest<OperationResult<RecipeIngredient>>
    {
        public Guid RecipeId { get; set; }
        public Guid UserProfileId { get; set; }
        public string IngredientName { get; set; }
    }
}

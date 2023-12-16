using Application.Models;
using Domain.Aggregates.RecipeAggregate;
using MediatR;

namespace Application.Recipes.Queries
{
    public class GetRecipeIngredients : IRequest<OperationResult<List<RecipeIngredient>>>
    {
        public Guid RecipeId { get; set; }
    }
}

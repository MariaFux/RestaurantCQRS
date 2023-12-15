using Application.Models;
using Domain.Aggregates.RecipeAggregate;
using MediatR;

namespace Application.Recipes.Queries
{
    public class GetRecipeById : IRequest<OperationResult<Recipe>>
    {
        public Guid RecipeId { get; set; }
    }
}

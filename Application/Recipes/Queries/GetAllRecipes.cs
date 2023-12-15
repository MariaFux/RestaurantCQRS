using Application.Models;
using Domain.Aggregates.RecipeAggregate;
using MediatR;

namespace Application.Recipes.Queries
{
    public class GetAllRecipes : IRequest<OperationResult<List<Recipe>>>
    {
    }
}

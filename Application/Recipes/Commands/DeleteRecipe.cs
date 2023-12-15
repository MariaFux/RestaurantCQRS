using Application.Models;
using Domain.Aggregates.RecipeAggregate;
using MediatR;

namespace Application.Recipes.Commands
{
    public class DeleteRecipe : IRequest<OperationResult<Recipe>>
    {
        public Guid RecipeId { get; set; }
    }
}

using Application.Models;
using Domain.Aggregates.RecipeAggregate;
using MediatR;

namespace Application.Recipes.Commands
{
    public class UpdateRecipeName : IRequest<OperationResult<Recipe>>
    {
        public string NewName { get; set; }
        public Guid RecipeId { get; set; }
    }
}

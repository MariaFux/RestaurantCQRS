using Application.Models;
using Domain.Aggregates.RecipeAggregate;
using MediatR;

namespace Application.Recipes.Commands
{
    public class CreateRecipe : IRequest<OperationResult<Recipe>>
    {
        public Guid UserProfileId { get; set; }
        public string Name { get; set; }
        public string TextContent { get; set; }
    }
}

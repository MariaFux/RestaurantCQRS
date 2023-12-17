using Application.Models;
using Domain.Aggregates.RecipeAggregate;
using MediatR;

namespace Application.Menus.Commands
{
    public class AddMenuRecipe : IRequest<OperationResult<Recipe>>
    {
        public Guid MenuId { get; set; }
        public Guid RecipeId { get; set; }
        public Guid UserProfileId { get; set; }
    }
}

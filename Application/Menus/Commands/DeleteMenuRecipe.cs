using Application.Models;
using Domain.Aggregates.MenuAggregate;
using MediatR;

namespace Application.Menus.Commands
{
    public class DeleteMenuRecipe : IRequest<OperationResult<Menu>>
    {
        public Guid MenuId { get; set; }
        public Guid RecipeId { get; set; }
    }
}

using Application.Models;
using Domain.Aggregates.MenuAggregate;
using MediatR;

namespace Application.Menus.Queries
{
    public class GetMenuRecipes : IRequest<OperationResult<Menu>>
    {
        public Guid MenuId { get; set; }
    }
}

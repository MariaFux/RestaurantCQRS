using Application.Models;
using Domain.Aggregates.MenuAggregate;
using MediatR;

namespace Application.Menus.Queries
{
    public class GetMenuById : IRequest<OperationResult<Menu>>
    {
        public Guid MenuId { get; set; }
    }
}

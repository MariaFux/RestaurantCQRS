using Application.Models;
using Domain.Aggregates.MenuAggregate;
using MediatR;

namespace Application.Menus.Queries
{
    public class GetAllMenus : IRequest<OperationResult<List<Menu>>>
    {
    }
}

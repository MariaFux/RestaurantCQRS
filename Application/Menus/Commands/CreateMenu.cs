using Application.Models;
using Domain.Aggregates.MenuAggregate;
using MediatR;

namespace Application.Menus.Commands
{
    public class CreateMenu : IRequest<OperationResult<Menu>>
    {
        public Guid UserProfileId { get; set; }
        public string Name { get; set; }
    }
}

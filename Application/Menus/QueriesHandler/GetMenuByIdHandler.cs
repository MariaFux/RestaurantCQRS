using Application.Enums;
using Application.Menus.Queries;
using Application.Models;
using Dal;
using Domain.Aggregates.MenuAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Menus.QueriesHandler
{
    public class GetMenuByIdHandler : IRequestHandler<GetMenuById, OperationResult<Menu>>
    {
        private readonly DataContext _dataContext;
        public GetMenuByIdHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<OperationResult<Menu>> Handle(GetMenuById request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<Menu>();
            var menu = await _dataContext.Menus
                .FirstOrDefaultAsync(r => r.MenuId == request.MenuId, cancellationToken);

            if (menu is null)
            {
                result.AddError(ErrorCode.NotFound, string.Format(MenuErrorMessages.MenuNotFound, request.MenuId));
                return result;
            }

            result.Payload = menu;
            return result;
        }
    }
}

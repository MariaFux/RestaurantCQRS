using Application.Enums;
using Application.Menus.Commands;
using Application.Models;
using Dal;
using Domain.Aggregates.MenuAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Menus.CommandHandlers
{
    public class DeleteMenuHandler : IRequestHandler<DeleteMenu, OperationResult<Menu>>
    {
        private readonly DataContext _dataContext;

        public DeleteMenuHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<OperationResult<Menu>> Handle(DeleteMenu request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<Menu>();

            try
            {
                var menu = await _dataContext.Menus.FirstOrDefaultAsync(r => r.MenuId == request.MenuId, cancellationToken);

                if (menu is null)
                {
                    result.AddError(ErrorCode.NotFound, string.Format(MenuErrorMessages.MenuNotFound, request.MenuId));
                    return result;
                }

                if (menu.UserProfileId != request.UserProfileId)
                {
                    result.AddError(ErrorCode.MenuDeleteNotPossible, MenuErrorMessages.MenuDeleteNotPossible);
                    return result;
                }

                _dataContext.Menus.Remove(menu);
                await _dataContext.SaveChangesAsync(cancellationToken);

                result.Payload = menu;
            }
            catch (Exception ex)
            {
                result.AddUnknowError(ex.Message);
            }

            return result;
        }
    }
}

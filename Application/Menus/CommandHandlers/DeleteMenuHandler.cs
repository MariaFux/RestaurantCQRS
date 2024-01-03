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
                    result.IsError = true;
                    var error = new Error
                    {
                        Code = ErrorCode.NotFound,
                        Message = $"No Menu found with ID {request.MenuId}"
                    };
                    result.Errors.Add(error);
                    return result;
                }

                if (menu.UserProfileId != request.UserProfileId)
                {
                    result.IsError = true;
                    var error = new Error
                    {
                        Code = ErrorCode.MenuDeleteNotPossible,
                        Message = $"Only the owner of a menu can delete it"
                    };
                    result.Errors.Add(error);
                    return result;
                }

                _dataContext.Menus.Remove(menu);
                await _dataContext.SaveChangesAsync(cancellationToken);

                result.Payload = menu;
            }
            catch (Exception ex)
            {
                var error = new Error
                {
                    Code = ErrorCode.UnknownError,
                    Message = $"{ex.Message}"
                };
                result.IsError = true;
                result.Errors.Add(error);
            }

            return result;
        }
    }
}

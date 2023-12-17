using Application.Enums;
using Application.Menus.Commands;
using Application.Models;
using Dal;
using Domain.Aggregates.MenuAggregate;
using Domain.Exceptions;
using MediatR;

namespace Application.Menus.CommandHandlers
{
    public class CreateMenuHandler : IRequestHandler<CreateMenu, OperationResult<Menu>>
    {
        private readonly DataContext _dataContext;

        public CreateMenuHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<OperationResult<Menu>> Handle(CreateMenu request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<Menu>();

            try
            {
                var menu = Menu.CreateMenu(request.UserProfileId, request.Name);
                _dataContext.Menus.Add(menu);
                await _dataContext.SaveChangesAsync();

                result.Payload = menu;
            }
            catch (MenuNotValidException ex)
            {
                result.IsError = true;
                ex.ValidationErrors.ForEach(e =>
                {
                    var error = new Error
                    {
                        Code = ErrorCode.ValidationError,
                        Message = $"{ex.Message}"
                    };
                    result.Errors.Add(error);
                });
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

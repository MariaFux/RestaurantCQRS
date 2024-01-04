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
                await _dataContext.SaveChangesAsync(cancellationToken);

                result.Payload = menu;
            }
            catch (MenuNotValidException ex)
            {
                ex.ValidationErrors.ForEach(e => result.AddError(ErrorCode.ValidationError, e));
            }
            catch (Exception ex)
            {
                result.AddUnknowError(ex.Message);
            }            

            return result;
        }
    }
}

using Application.Enums;
using Application.Menus.Queries;
using Application.Models;
using Dal;
using Domain.Aggregates.MenuAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Menus.QueriesHandler
{
    public class GetMenuRecipesHandler : IRequestHandler<GetMenuRecipes, OperationResult<Menu>>
    {
        private readonly DataContext _dataContext;

        public GetMenuRecipesHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<OperationResult<Menu>> Handle(GetMenuRecipes request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<Menu>();
            try
            {
                var menu = await _dataContext.Menus
                    .Include(m => m.Recipes)
                        .ThenInclude(r => r.Ingredients)
                    .Include(m => m.UserProfile)
                    .FirstOrDefaultAsync(m => m.MenuId == request.MenuId, cancellationToken);

                result.Payload = menu;
            }
            catch (Exception ex)
            {
                var error = new Error { Code = ErrorCode.UnknownError, Message = $"{ex.Message}" };
                result.IsError = true;
                result.Errors.Add(error);
            }

            return result;
        }
    }
}

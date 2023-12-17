using Application.Enums;
using Application.Menus.Commands;
using Application.Models;
using Dal;
using Domain.Aggregates.MenuAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Menus.CommandHandlers
{
    public class DeleteMenuRecipeHandler : IRequestHandler<DeleteMenuRecipe, OperationResult<Menu>>
    {
        private readonly DataContext _dataContext;

        public DeleteMenuRecipeHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<OperationResult<Menu>> Handle(DeleteMenuRecipe request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<Menu>();

            try
            {
                var menu = _dataContext.Menus
                    .Include(m => m.Recipes)
                    .FirstOrDefault(m => m.MenuId == request.MenuId);
                var recipeToDelete = menu.Recipes.FirstOrDefault(mr => mr.RecipeId == request.RecipeId);

                if (menu is null)
                {
                    result.IsError = true;
                    var error = new Error
                    {
                        Code = ErrorCode.NotFound,
                        Message = $"No Menus found with ID {request.MenuId}"
                    };
                    result.Errors.Add(error);
                    return result;
                }

                if (recipeToDelete is null)
                {
                    result.IsError = true;
                    var error = new Error
                    {
                        Code = ErrorCode.NotFound,
                        Message = $"No Recipes found with ID {request.RecipeId}"
                    };
                    result.Errors.Add(error);
                    return result;
                }

                menu.RemoveRecipe(recipeToDelete);

                _dataContext.Menus.Update(menu);
                await _dataContext.SaveChangesAsync();

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

using Application.Enums;
using Application.Menus.Commands;
using Application.Models;
using Dal;
using Domain.Aggregates.RecipeAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Menus.CommandHandlers
{
    public class AddMenuRecipeHandler : IRequestHandler<AddMenuRecipe, OperationResult<Recipe>>
    {
        private readonly DataContext _dataContext;

        public AddMenuRecipeHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<OperationResult<Recipe>> Handle(AddMenuRecipe request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<Recipe>();

            try
            {
                var menu = await _dataContext.Menus.FirstOrDefaultAsync(m => m.MenuId == request.MenuId);                

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

                if (menu.UserProfileId != request.UserProfileId)
                {
                    result.IsError = true;
                    var error = new Error
                    {
                        Code = ErrorCode.AddRecipeToMenuNotPossible,
                        Message = $"Add recipe to menu not possible because it's not the menu owner that initiates the add"
                    };
                    result.Errors.Add(error);
                    return result;
                }

                var recipe = await _dataContext.Recipes.FirstOrDefaultAsync(r => r.RecipeId == request.RecipeId);

                if (recipe is null)
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

                var menuRecipe = menu.AddRecipe(recipe);

                menu.AddRecipe(menuRecipe);

                _dataContext.Menus.Update(menu);
                await _dataContext.SaveChangesAsync();

                result.Payload = menuRecipe;
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

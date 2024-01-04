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
                var menu = await _dataContext.Menus.FirstOrDefaultAsync(m => m.MenuId == request.MenuId, cancellationToken);                

                if (menu is null)
                {
                    result.AddError(ErrorCode.NotFound, string.Format(MenuErrorMessages.MenuNotFound, request.MenuId));
                    return result;
                }

                if (menu.UserProfileId != request.UserProfileId)
                {
                    result.AddError(ErrorCode.AddRecipeToMenuNotPossible, MenuErrorMessages.AddRecipeToMenuNotPossible);                    
                    return result;
                }

                var recipe = await _dataContext.Recipes.FirstOrDefaultAsync(r => r.RecipeId == request.RecipeId, cancellationToken);

                if (recipe is null)
                {
                    result.AddError(ErrorCode.NotFound, string.Format(MenuErrorMessages.RecipeNotFound, request.RecipeId));
                    return result;
                }

                var menuRecipe = menu.AddRecipe(recipe);

                menu.AddRecipe(menuRecipe);

                _dataContext.Menus.Update(menu);
                await _dataContext.SaveChangesAsync(cancellationToken);

                result.Payload = menuRecipe;
            }
            catch (Exception ex)
            {
                result.AddUnknowError(ex.Message);
            }

            return result;
        }
    }
}

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
                var menu = await _dataContext.Menus
                    .Include(m => m.Recipes)
                    .FirstOrDefaultAsync(m => m.MenuId == request.MenuId, cancellationToken);
                var recipeToDelete = menu.Recipes.FirstOrDefault(mr => mr.RecipeId == request.RecipeId);

                if (menu is null)
                {
                    result.AddError(ErrorCode.NotFound, string.Format(MenuErrorMessages.MenuNotFound, request.MenuId));
                    return result;
                }

                if (recipeToDelete is null)
                {
                    result.AddError(ErrorCode.NotFound, string.Format(MenuErrorMessages.RecipeNotFound, request.RecipeId));
                    return result;
                }

                menu.RemoveRecipe(recipeToDelete);

                _dataContext.Menus.Update(menu);
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

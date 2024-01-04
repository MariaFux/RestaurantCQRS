using Application.Enums;
using Application.Models;
using Application.Recipes.Commands;
using Dal;
using Domain.Aggregates.RecipeAggregate;
using Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Recipes.CommandHandlers
{
    public class AddRecipeIngredientHandler : IRequestHandler<AddRecipeIngredient, OperationResult<RecipeIngredient>>
    {
        private readonly DataContext _dataContext;

        public AddRecipeIngredientHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<OperationResult<RecipeIngredient>> Handle(AddRecipeIngredient request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<RecipeIngredient>();

            try
            {
                var recipe = await _dataContext.Recipes.FirstOrDefaultAsync(r => r.RecipeId == request.RecipeId, cancellationToken);

                if (recipe is null)
                {
                    result.AddError(ErrorCode.NotFound, string.Format(RecipeErrorMessages.RecipeNotFound, request.RecipeId));
                    return result;
                }

                var ingredient = RecipeIngredient.CreateRecipeIngredient(request.RecipeId, request.IngredientName, request.UserProfileId);
                
                recipe.AddIngredient(ingredient);
                
                _dataContext.Recipes.Update(recipe);
                await _dataContext.SaveChangesAsync(cancellationToken);

                result.Payload = ingredient;
            }
            catch (RecipeIngredientNotValidException ex)
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

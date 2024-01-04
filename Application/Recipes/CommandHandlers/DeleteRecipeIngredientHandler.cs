using Application.Enums;
using Application.Models;
using Application.Recipes.Commands;
using Dal;
using Domain.Aggregates.RecipeAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Recipes.CommandHandlers
{
    public class DeleteRecipeIngredientHandler : IRequestHandler<DeleteRecipeIngredient, OperationResult<RecipeIngredient>>
    {
        private readonly DataContext _dataContext;

        public DeleteRecipeIngredientHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<OperationResult<RecipeIngredient>> Handle(DeleteRecipeIngredient request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<RecipeIngredient>();

            try
            {
                var recipe = await _dataContext.Recipes
                    .Include(r => r.Ingredients)
                    .FirstOrDefaultAsync(r => r.RecipeId == request.RecipeId, cancellationToken);
                var ingredient = recipe.Ingredients.FirstOrDefault(ri => ri.IngredientId == request.IngredientId);

                if (recipe is null)
                {
                    result.AddError(ErrorCode.NotFound, string.Format(RecipeErrorMessages.RecipeNotFound, request.RecipeId));
                    return result;
                }

                if (ingredient is null)
                {
                    result.AddError(ErrorCode.NotFound, string.Format(RecipeErrorMessages.IngredientNotFound, request.IngredientId));
                    return result;
                }

                recipe.RemoveIngredient(ingredient);

                _dataContext.Recipes.Update(recipe);
                await _dataContext.SaveChangesAsync(cancellationToken);

                result.Payload = ingredient;
            }
            catch (Exception ex)
            {
                result.AddUnknowError(ex.Message);
            }

            return result;
        }
    }
}

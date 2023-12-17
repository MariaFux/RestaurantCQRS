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
                    .FirstOrDefaultAsync(r => r.RecipeId == request.RecipeId);
                var ingredient = recipe.Ingredients.FirstOrDefault(ri => ri.IngredientId == request.IngredientId);

                if (recipe is null)
                {
                    result.IsError = true;
                    var error = new Error
                    {
                        Code = ErrorCode.NotFound,
                        Message = $"No Recipe found with ID {request.RecipeId}"
                    };
                    result.Errors.Add(error);
                    return result;
                }

                if (ingredient is null)
                {
                    result.IsError = true;
                    var error = new Error
                    {
                        Code = ErrorCode.NotFound,
                        Message = $"No Ingredient found with ID {request.IngredientId}"
                    };
                    result.Errors.Add(error);
                    return result;
                }

                recipe.RemoveIngredient(ingredient);

                _dataContext.Recipes.Update(recipe);
                await _dataContext.SaveChangesAsync();

                result.Payload = ingredient;
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

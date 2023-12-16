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
                var recipe = await _dataContext.Recipes.FirstOrDefaultAsync(r => r.RecipeId == request.RecipeId);

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

                var ingredient = RecipeIngredient.CreateRecipeIngredient(request.RecipeId, request.IngredientName, request.UserProfileId);
                
                recipe.AddIngredient(ingredient);
                
                _dataContext.Recipes.Update(recipe);
                await _dataContext.SaveChangesAsync();

                result.Payload = ingredient;
            }
            catch (RecipeIngredientNotValidException ex)
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

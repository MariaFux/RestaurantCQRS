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
    public class UpdateRecipeTextHandler : IRequestHandler<UpdateRecipeText, OperationResult<Recipe>>
    {
        private readonly DataContext _dataContext;

        public UpdateRecipeTextHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<OperationResult<Recipe>> Handle(UpdateRecipeText request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<Recipe>();

            try
            {
                var recipe = await _dataContext.Recipes.FirstOrDefaultAsync(r => r.RecipeId == request.RecipeId, cancellationToken);

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

                recipe.UpdateRecipeText(request.NewText);
                
                await _dataContext.SaveChangesAsync(cancellationToken);
                result.Payload = recipe;
            }
            catch (RecipeNotValidException ex)
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

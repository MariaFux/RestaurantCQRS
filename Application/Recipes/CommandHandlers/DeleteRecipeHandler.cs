using Application.Enums;
using Application.Models;
using Application.Recipes.Commands;
using Dal;
using Domain.Aggregates.RecipeAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Recipes.CommandHandlers
{
    public class DeleteRecipeHandler : IRequestHandler<DeleteRecipe, OperationResult<Recipe>>
    {
        private readonly DataContext _dataContext;

        public DeleteRecipeHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<OperationResult<Recipe>> Handle(DeleteRecipe request, CancellationToken cancellationToken)
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

                _dataContext.Recipes.Remove(recipe);
                await _dataContext.SaveChangesAsync(cancellationToken);

                result.Payload = recipe;
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

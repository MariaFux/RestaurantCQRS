using Application.Enums;
using Application.Models;
using Application.Recipes.Commands;
using Dal;
using Domain.Aggregates.RecipeAggregate;
using Domain.Exceptions;
using MediatR;

namespace Application.Recipes.CommandHandlers
{
    public class CreateRecipeHandler : IRequestHandler<CreateRecipe, OperationResult<Recipe>>
    {
        private readonly DataContext _dataContext;

        public CreateRecipeHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<OperationResult<Recipe>> Handle(CreateRecipe request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<Recipe>();
            try
            {
                var recipe = Recipe.CreateRecipe(request.UserProfileId, request.Name, request.TextContent);
                _dataContext.Recipes.Add(recipe);
                await _dataContext.SaveChangesAsync();

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

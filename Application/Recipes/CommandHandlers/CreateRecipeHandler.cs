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
                await _dataContext.SaveChangesAsync(cancellationToken);

                result.Payload = recipe;
            } 
            catch (RecipeNotValidException ex)
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

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
    public class UpdateRecipeNameHandler : IRequestHandler<UpdateRecipeName, OperationResult<Recipe>>
    {
        private readonly DataContext _dataContext;

        public UpdateRecipeNameHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<OperationResult<Recipe>> Handle(UpdateRecipeName request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<Recipe>();

            try
            {
                var recipe = await _dataContext.Recipes.FirstOrDefaultAsync(r => r.RecipeId == request.RecipeId, cancellationToken);

                if (recipe is null)
                {
                    result.AddError(ErrorCode.NotFound, string.Format(RecipeErrorMessages.RecipeNotFound, request.RecipeId));
                    return result;
                }

                recipe.UpdateRecipeName(request.NewName);

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

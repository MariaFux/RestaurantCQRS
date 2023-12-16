using Application.Enums;
using Application.Models;
using Application.Recipes.Queries;
using Dal;
using Domain.Aggregates.RecipeAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Recipes.QueriesHandler
{
    public class GetRecipeIngredientsHandler : IRequestHandler<GetRecipeIngredients, OperationResult<List<RecipeIngredient>>>
    {
        private readonly DataContext _dataContext;

        public GetRecipeIngredientsHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<OperationResult<List<RecipeIngredient>>> Handle(GetRecipeIngredients request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<List<RecipeIngredient>>();
            try
            {
                var recipe = await _dataContext.Recipes
                    .Include(r => r.Ingredients)
                    .FirstOrDefaultAsync(r => r.RecipeId == request.RecipeId);

                result.Payload = recipe.Ingredients.ToList();
            }
            catch (Exception ex)
            {
                var error = new Error { Code = ErrorCode.UnknownError, Message = $"{ex.Message}" };
                result.IsError = true;
                result.Errors.Add(error);
            }

            return result;
        }
    }
}

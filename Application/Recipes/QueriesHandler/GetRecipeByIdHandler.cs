using Application.Enums;
using Application.Models;
using Application.Recipes.Queries;
using Dal;
using Domain.Aggregates.RecipeAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Recipes.QueriesHandler
{
    public class GetRecipeByIdHandler : IRequestHandler<GetRecipeById, OperationResult<Recipe>>
    {
        private readonly DataContext _dataContext;
        public GetRecipeByIdHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<OperationResult<Recipe>> Handle(GetRecipeById request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<Recipe>();
            var recipe = await _dataContext.Recipes
                .FirstOrDefaultAsync(r => r.RecipeId == request.RecipeId, cancellationToken);

            if (recipe is null)
            {
                result.AddError(ErrorCode.NotFound, string.Format(RecipeErrorMessages.RecipeNotFound, request.RecipeId));
                return result;
            }

            result.Payload = recipe;
            return result;
        }
    }
}

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
                result.IsError = true;
                var error = new Error
                {
                    Code = ErrorCode.NotFound,
                    Message = $"No Recipe found with ID {request.RecipeId}"
                };
                result.Errors.Add(error);
                return result;
            }

            result.Payload = recipe;
            return result;
        }
    }
}

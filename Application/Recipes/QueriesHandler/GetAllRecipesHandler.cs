using Application.Models;
using Application.Recipes.Queries;
using Dal;
using Domain.Aggregates.RecipeAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Recipes.QueriesHandler
{
    public class GetAllRecipesHandler : IRequestHandler<GetAllRecipes, OperationResult<List<Recipe>>>
    {
        private readonly DataContext _dataContext;
        public GetAllRecipesHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<OperationResult<List<Recipe>>> Handle(GetAllRecipes request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<List<Recipe>>();
            try
            {                
                var recipes = await _dataContext.Recipes.ToListAsync(cancellationToken);
                result.Payload = recipes;
            }
            catch (Exception ex)
            {
                result.AddUnknowError(ex.Message);
            }
            return result;
        }
    }
}

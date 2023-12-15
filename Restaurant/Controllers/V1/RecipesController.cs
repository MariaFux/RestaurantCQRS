using Application.Contracts.Recipes.Requests;
using Application.Contracts.Recipes.Responses;
using Application.Recipes.Commands;
using Application.Recipes.Queries;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Filters;

namespace Restaurant.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route(ApiRoutes.BaseRoute)]
    [ApiController]
    public class RecipesController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public RecipesController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRecipes()
        {
            var result = await _mediator.Send(new GetAllRecipes());
            var mapped = _mapper.Map<List<RecipeResponse>>(result.Payload);

            return result.IsError ? HandleErrorResponse(result.Errors) : Ok(mapped);
        }

        [HttpGet]
        [Route(ApiRoutes.Recipes.IdRoute)]
        [ValidateGuid("id")]
        public async Task<IActionResult> GetById(string id)
        {
            var recipeId = Guid.Parse(id);
            var query = new GetRecipeById() { RecipeId = recipeId };
            var result = await _mediator.Send(query);
            var mapped = _mapper.Map<RecipeResponse>(result.Payload);

            return result.IsError ? HandleErrorResponse(result.Errors) : Ok(mapped);
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> CreateRecipe([FromBody] RecipeCreate newRecipe)
        {
            var command = new CreateRecipe()
            {
                UserProfileId = newRecipe.UserProfileId,
                Name = newRecipe.Name,
                TextContent = newRecipe.TextContent
            };

            var result = await _mediator.Send(command);
            var mapped = _mapper.Map<RecipeResponse>(result.Payload);

            return result.IsError ? HandleErrorResponse(result.Errors) 
                : CreatedAtAction(nameof(GetById), new { id = result.Payload.UserProfileId }, mapped);
        }

        [HttpPatch]
        [Route(ApiRoutes.Recipes.IdRoute)]
        [ValidateGuid("id")]
        [ValidateModel]
        public async Task<IActionResult> UpdateRecipeName([FromBody] RecipeUpdateName updatedRecipeName, string id)
        {
            var command = new UpdateRecipeName()
            {
                NewName = updatedRecipeName.Name,
                RecipeId = Guid.Parse(id)
            };

            var result = await _mediator.Send(command);

            return result.IsError ? HandleErrorResponse(result.Errors) : NoContent();
        }

        [HttpPatch]
        [Route(ApiRoutes.Recipes.IdRoute)]
        [ValidateGuid("id")]
        [ValidateModel]
        public async Task<IActionResult> UpdateRecipeText([FromBody] RecipeUpdateText updatedRecipeText, string id)
        {
            var command = new UpdateRecipeText()
            {
                NewText = updatedRecipeText.Text,
                RecipeId = Guid.Parse(id)
            };

            var result = await _mediator.Send(command);

            return result.IsError ? HandleErrorResponse(result.Errors) : NoContent();
        }

        [HttpDelete]
        [Route(ApiRoutes.Recipes.IdRoute)]
        [ValidateGuid("id")]
        public async Task<IActionResult> DeleteRecipe(string id)
        {
            var command = new DeleteRecipe() { RecipeId = Guid.Parse(id) };
            var result = await _mediator.Send(command);

            return result.IsError ? HandleErrorResponse(result.Errors) : NoContent();
        }
    }
}

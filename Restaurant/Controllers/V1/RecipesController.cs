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
        public async Task<IActionResult> GetAllRecipes(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetAllRecipes(), cancellationToken);
            var mapped = _mapper.Map<List<RecipeResponse>>(result.Payload);

            return result.IsError ? HandleErrorResponse(result.Errors) : Ok(mapped);
        }

        [HttpGet]
        [Route(ApiRoutes.Recipes.IdRoute)]
        [ValidateGuid("id")]
        public async Task<IActionResult> GetById(string id, CancellationToken cancellationToken)
        {
            var recipeId = Guid.Parse(id);
            var query = new GetRecipeById() { RecipeId = recipeId };
            var result = await _mediator.Send(query, cancellationToken);
            var mapped = _mapper.Map<RecipeResponse>(result.Payload);

            return result.IsError ? HandleErrorResponse(result.Errors) : Ok(mapped);
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> CreateRecipe([FromBody] RecipeCreate newRecipe, CancellationToken cancellationToken)
        {
            var userProfileId = HttpContext.GetUserProfileIdClaimValue();

            var command = new CreateRecipe()
            {
                UserProfileId = userProfileId,
                Name = newRecipe.Name,
                TextContent = newRecipe.TextContent
            };

            var result = await _mediator.Send(command, cancellationToken);
            var mapped = _mapper.Map<RecipeResponse>(result.Payload);

            return result.IsError ? HandleErrorResponse(result.Errors) 
                : CreatedAtAction(nameof(GetById), new { id = result.Payload.UserProfileId }, mapped);
        }

        [HttpPatch]
        [Route(ApiRoutes.Recipes.IdRoute)]
        [ValidateGuid("id")]
        [ValidateModel]
        public async Task<IActionResult> UpdateRecipeName([FromBody] RecipeUpdateName updatedRecipeName, string id, CancellationToken cancellationToken)
        {
            var command = new UpdateRecipeName()
            {
                NewName = updatedRecipeName.Name,
                RecipeId = Guid.Parse(id)
            };

            var result = await _mediator.Send(command, cancellationToken);

            return result.IsError ? HandleErrorResponse(result.Errors) : NoContent();
        }

        [HttpPatch]
        [Route(ApiRoutes.Recipes.IdRoute)]
        [ValidateGuid("id")]
        [ValidateModel]
        public async Task<IActionResult> UpdateRecipeText([FromBody] RecipeUpdateText updatedRecipeText, string id, CancellationToken cancellationToken)
        {
            var command = new UpdateRecipeText()
            {
                NewText = updatedRecipeText.Text,
                RecipeId = Guid.Parse(id)
            };

            var result = await _mediator.Send(command, cancellationToken);

            return result.IsError ? HandleErrorResponse(result.Errors) : NoContent();
        }

        [HttpDelete]
        [Route(ApiRoutes.Recipes.IdRoute)]
        [ValidateGuid("id")]
        public async Task<IActionResult> DeleteRecipe(string id, CancellationToken cancellationToken)
        {
            var command = new DeleteRecipe() { RecipeId = Guid.Parse(id) };
            var result = await _mediator.Send(command, cancellationToken);

            return result.IsError ? HandleErrorResponse(result.Errors) : NoContent();
        }

        [HttpGet]
        [Route(ApiRoutes.Recipes.RecipeIngredients)]
        [ValidateGuid("recipeId")]
        public async Task<IActionResult> GetIngredientsByRecipeId(string recipeId, CancellationToken cancellationToken)
        {
            var query = new GetRecipeIngredients() { RecipeId = Guid.Parse(recipeId) };
            var result = await _mediator.Send(query, cancellationToken);

            if (result.IsError) return HandleErrorResponse(result.Errors);

            var ingredients = _mapper.Map<List<RecipeIngredientResponse>>(result.Payload);
            return Ok(ingredients);
        }

        [HttpPost]
        [Route(ApiRoutes.Recipes.RecipeIngredients)]
        [ValidateGuid("recipeId")]
        [ValidateModel]
        public async Task<IActionResult> AddIngredientToRecipe(string recipeId, [FromBody] RecipeIngredientCreate ingredient, CancellationToken cancellationToken)
        {
            var userProfileId = HttpContext.GetUserProfileIdClaimValue();

            var command = new AddRecipeIngredient()
            {
                RecipeId = Guid.Parse(recipeId),
                UserProfileId = userProfileId,
                IngredientName = ingredient.Name
            };

            var result = await _mediator.Send(command, cancellationToken);

            if (result.IsError) return HandleErrorResponse(result.Errors);

            var newIngredient = _mapper.Map<RecipeIngredientResponse>(result.Payload);
            return Ok(newIngredient);
        }

        [HttpDelete]
        [Route(ApiRoutes.Recipes.IdRoute)]
        [ValidateGuid("id")]
        [ValidateGuid("ingredientId")]
        public async Task<IActionResult> RemoveIngredientFromRecipe(string id, [FromQuery, BindRequired] string ingredientId, CancellationToken cancellationToken)
        {
            var command = new DeleteRecipeIngredient() { RecipeId = Guid.Parse(id), IngredientId = Guid.Parse(ingredientId) };
            var result = await _mediator.Send(command, cancellationToken);

            return result.IsError ? HandleErrorResponse(result.Errors) : NoContent();
        }
    }
}

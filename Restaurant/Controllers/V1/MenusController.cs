namespace Restaurant.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route(ApiRoutes.BaseRoute)]
    [ApiController]
    [Authorize]
    public class MenusController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public MenusController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMenus()
        {
            var result = await _mediator.Send(new GetAllMenus());
            var mapped = _mapper.Map<List<MenuResponse>>(result.Payload);

            return result.IsError ? HandleErrorResponse(result.Errors) : Ok(mapped);
        }

        [HttpGet]
        [Route(ApiRoutes.Menus.IdRoute)]
        [ValidateGuid("id")]
        public async Task<IActionResult> GetById(string id)
        {
            var menuId = Guid.Parse(id);
            var query = new GetMenuById() { MenuId = menuId };
            var result = await _mediator.Send(query);
            var mapped = _mapper.Map<MenuResponse>(result.Payload);

            return result.IsError ? HandleErrorResponse(result.Errors) : Ok(mapped);
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> CreateMenu([FromBody] MenuCreate newMenu)
        {
            var userProfileId = HttpContext.GetUserProfileIdClaimValue();

            var command = new CreateMenu()
            {
                UserProfileId = userProfileId,
                Name = newMenu.Name
            };

            var result = await _mediator.Send(command);
            var mapped = _mapper.Map<MenuResponse>(result.Payload);

            return result.IsError ? HandleErrorResponse(result.Errors)
                : CreatedAtAction(nameof(GetById), new { id = result.Payload.UserProfileId }, mapped);
        }

        [HttpDelete]
        [Route(ApiRoutes.Menus.IdRoute)]
        [ValidateGuid("id")]
        public async Task<IActionResult> DeleteMenu(string id)
        {
            var userProfileId = HttpContext.GetUserProfileIdClaimValue();

            var command = new DeleteMenu() { MenuId = Guid.Parse(id), UserProfileId = userProfileId };
            var result = await _mediator.Send(command);

            return result.IsError ? HandleErrorResponse(result.Errors) : NoContent();
        }

        [HttpGet]
        [Route(ApiRoutes.Menus.MenusRecipes)]
        [ValidateGuid("menuId")]
        public async Task<IActionResult> GetRecipesByMenuId(string menuId)
        {
            var query = new GetMenuRecipes() { MenuId = Guid.Parse(menuId) };
            var result = await _mediator.Send(query);

            if (result.IsError) return HandleErrorResponse(result.Errors);

            var menu = _mapper.Map<MenuRecipeResponse>(result.Payload);
            return Ok(menu);
        }

        [HttpPost]
        [Route(ApiRoutes.Menus.MenusRecipes)]
        [ValidateGuid("menuId")]
        [ValidateGuid("recipeId")]
        [ValidateModel]
        public async Task<IActionResult> AddRecipeToMenu(string menuId, [FromQuery, BindRequired] string recipeId)
        {
            var userProfileId = HttpContext.GetUserProfileIdClaimValue();

            var command = new AddMenuRecipe()
            {
                MenuId = Guid.Parse(menuId),
                RecipeId = Guid.Parse(recipeId),
                UserProfileId = userProfileId
            };

            var result = await _mediator.Send(command);

            if (result.IsError) return HandleErrorResponse(result.Errors);

            var newRecipe = _mapper.Map<MenuRecipeResponse>(result.Payload);
            return Ok(newRecipe);
        }

        [HttpDelete]
        [Route(ApiRoutes.Menus.IdRoute)]
        [ValidateGuid("id")]
        [ValidateGuid("recipeId")]
        public async Task<IActionResult> RemoveRecipeFromMenu(string id, [FromQuery, BindRequired] string recipeId)
        {
            var command = new DeleteMenuRecipe() { MenuId = Guid.Parse(id), RecipeId = Guid.Parse(recipeId) };
            var result = await _mediator.Send(command);

            return result.IsError ? HandleErrorResponse(result.Errors) : NoContent();
        }
    }
}

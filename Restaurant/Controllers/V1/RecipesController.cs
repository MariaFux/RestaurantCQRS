using Microsoft.AspNetCore.Mvc;

namespace Restaurant.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route(ApiRoutes.BaseRoute)]
    [ApiController]
    public class RecipesController : Controller
    {
        [HttpGet]
        [Route(ApiRoutes.Recipes.IdRoute)]
        public IActionResult GetById(int id)
        {
            return Ok();
        }
    }
}

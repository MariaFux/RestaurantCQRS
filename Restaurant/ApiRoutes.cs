namespace Restaurant
{
    public class ApiRoutes
    {
        public const string BaseRoute = "api/v{version:apiVersion}/[controller]/[action]";

        public class UserProfiles
        {
            public const string IdRoute = "{id}";
        }

        public class Recipes
        {
            public const string IdRoute = "{id}";
            public const string RecipeIngredients = "{recipeId}/ingredients";
            public const string IngredientById = "{recipeId}/ingredients/{ingredientId}";
        }

        public class Menus
        {
            public const string IdRoute = "{id}";
        }
    }
}

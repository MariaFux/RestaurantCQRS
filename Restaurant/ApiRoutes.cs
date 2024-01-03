namespace Restaurant
{
    public class ApiRoutes
    {
        public const string BaseRoute = "api/v{version:apiVersion}/[controller]/[action]";

        public static class UserProfiles
        {
            public const string IdRoute = "{id}";
        }

        public static class Recipes
        {
            public const string IdRoute = "{id}";
            public const string RecipeIngredients = "{recipeId}/ingredients";
            public const string IngredientById = "{recipeId}/ingredients/{ingredientId}";
        }

        public static class Menus
        {
            public const string IdRoute = "{id}";
            public const string MenusRecipes = "{menuId}/recipes";
            public const string RecipeById = "{menuId}/recipes/{recipeId}";
        }

        public static class Identity
        {
            public const string Login = "login";
            public const string Registration = "registration";
        } 
    }
}

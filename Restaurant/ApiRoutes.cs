namespace Restaurant
{
    public class ApiRoutes
    {
        public const string BaseRoute = "api/v{version:apiVersion}/[controller]";

        public class UserProfiles
        {
            public const string IdRoute = "{id}";
        }

        public class Recipes
        {
            public const string IdRoute = "{id}";
        }

        public class Menus
        {
            public const string IdRoute = "{id}";
        }
    }
}

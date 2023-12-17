using Domain.Aggregates.RecipeAggregate;
using Domain.Aggregates.UserProfileAggregate;

namespace Domain.Aggregates.MenuAggregate
{
    public class Menu
    {
        private readonly List<Recipe> _recipes = new List<Recipe>();
        private Menu()
        {
        }
        public Guid MenuId { get; private set; }
        public Guid UserProfileId { get; private set; }
        public string Name { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public DateTime LastModified { get; private set; }
        public UserProfile UserProfile { get; private set; }
        public IEnumerable<Recipe> Recipes { get { return _recipes; } }

        // Factory method
        /// <summary>
        /// Create a new menu instance
        /// </summary>
        /// <param name="userProfileId">User profile ID</param>
        /// <param name="name">Menu Name</param>
        /// <returns><see cref="Menu"/></returns>
        public static Menu CreateMenu(Guid userProfileId, string name)
        {
            return new Menu
            {
                UserProfileId = userProfileId,
                Name = name,
                CreatedDate = DateTime.UtcNow,
                LastModified = DateTime.UtcNow
            };
        }

        //public methods
        public Recipe AddRecipe(Recipe newRecipe)
        {
            _recipes.Add(newRecipe);
            LastModified = DateTime.UtcNow;

            return newRecipe;
        }

        public void RemoveRecipe(Recipe toRemoveRecipe)
        {
            _recipes.Remove(toRemoveRecipe);
            LastModified = DateTime.UtcNow;
        }
    }
}

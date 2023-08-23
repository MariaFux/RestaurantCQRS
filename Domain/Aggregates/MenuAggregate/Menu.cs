using Domain.Aggregates.RecipeAggregate;

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
        public DateTime CreatedDate { get; private set; }
        public DateTime LastModified { get; private set; }
        public IEnumerable<Recipe> Recipes { get { return _recipes; } }

        // Factory method
        public static Menu CreateMenu(Guid userProfileId)
        {
            return new Menu
            {
                UserProfileId = userProfileId,
                CreatedDate = DateTime.UtcNow,
                LastModified = DateTime.UtcNow
            };
        }

        //public methods
        public void AddRecipe(Recipe newRecipe)
        {
            _recipes.Add(newRecipe);
            LastModified = DateTime.UtcNow;
        }

        public void RemoveRecipe(Recipe toRemoveRecipe)
        {
            _recipes.Remove(toRemoveRecipe);
            LastModified = DateTime.UtcNow;
        }
    }
}

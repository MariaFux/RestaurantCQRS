using Domain.Aggregates.UserProfileAggregate;

namespace Domain.Aggregates.RecipeAggregate
{
    public class Recipe
    {
        private Recipe()
        {
        }
        public Guid RecipeId { get; private set; }
        public Guid UserProfileId { get; private set; }
        public UserProfile UserProfile { get; private set; }
        public string Name { get; private set; }
        public string TextContent { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public DateTime LastModified { get; private set; }

        //Factory method
        public static Recipe CreateRecipe(Guid userProfileId, string name, string textContent)
        {
            return new Recipe
            {
                UserProfileId = userProfileId,
                Name = name,
                TextContent = textContent,
                CreatedDate = DateTime.UtcNow,
                LastModified = DateTime.UtcNow
            };
        }

        //public methods
        public void UpdateRecipeName(string newName)
        {
            Name = newName;
            LastModified = DateTime.UtcNow;
        }

        public void UpdateRecipeText(string newText)
        {
            TextContent = newText;
            LastModified = DateTime.UtcNow;
        }
    }
}

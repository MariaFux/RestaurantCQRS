using Domain.Aggregates.UserProfileAggregate;
using Domain.Exceptions;
using Domain.Validators.RecipeValidators;

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
        /// <summary>
        /// Creates a new recipe instance
        /// </summary>
        /// <param name="userProfileId">User profile ID</param>
        /// <param name="name">Recipe Name</param>
        /// <param name="textContent">Recipe Content</param>
        /// <returns><see cref="Recipe"/></returns>
        /// <exception cref="RecipeNotValidException"></exception>
        public static Recipe CreateRecipe(Guid userProfileId, string name, string textContent)
        {
            var validator = new RecipeValidator();
            var objectToValidate = new Recipe
            {
                UserProfileId = userProfileId,
                Name = name,
                TextContent = textContent,
                CreatedDate = DateTime.UtcNow,
                LastModified = DateTime.UtcNow
            };

            var validationResult = validator.Validate(objectToValidate);

            if (validationResult.IsValid) return objectToValidate;

            var exception = new RecipeNotValidException("Recipe is not valid");
            
            validationResult.Errors.ForEach(vr => exception.ValidationErrors.Add(vr.ErrorMessage));
            throw exception;
        }

        //public methods
        public void UpdateRecipeName(string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
            {
                var exception = new RecipeNotValidException("Cannot update recipe name." +
                    "Name is not valid");
                exception.ValidationErrors.Add("The provided name is either null or contains only white space");
                throw exception; 
            }
            Name = newName;
            LastModified = DateTime.UtcNow;
        }

        public void UpdateRecipeText(string newText)
        {
            if (string.IsNullOrWhiteSpace(newText))
            {
                var exception = new RecipeNotValidException("Cannot update recipe text." +
                    "Text is not valid");
                exception.ValidationErrors.Add("The provided text is either null or contains only white space");
                throw exception;
            }
            TextContent = newText;
            LastModified = DateTime.UtcNow;
        }
    }
}

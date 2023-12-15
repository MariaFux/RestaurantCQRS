using Domain.Exceptions;
using Domain.Validators.RecipeValidators;

namespace Domain.Aggregates.RecipeAggregate
{
    public class RecipeIngredient
    {
        private RecipeIngredient()
        {
        }
        public Guid IngredientId { get; private set; }
        public Guid RecipeId { get; private set; }
        public string Name { get; private set; }
        public Guid UserProfileId { get; private set; }
        public DateTime DateCreated { get; private set; }
        public DateTime LastModified { get; private set; }

        public static RecipeIngredient CreateRecipeIngredient(Guid recipeId, string name, Guid userProfileId)
        {
            var validator = new RecipeIngredientValidator();
            var objectToValidate = new RecipeIngredient
            {
                RecipeId = recipeId,
                Name = name,
                UserProfileId = userProfileId,
                DateCreated = DateTime.UtcNow,
                LastModified = DateTime.UtcNow
            };

            var validationResult = validator.Validate(objectToValidate);

            if (validationResult.IsValid) return objectToValidate;

            var exception = new RecipeIngredientNotValidException("Recipe ingredient is not valid");

            validationResult.Errors.ForEach(vr => exception.ValidationErrors.Add(vr.ErrorMessage));
            throw exception;
        }

        //public methods
        public void UpdateIngredientName(string newName)
        {
            Name = newName;
            LastModified = DateTime.UtcNow;
        }
    }
}

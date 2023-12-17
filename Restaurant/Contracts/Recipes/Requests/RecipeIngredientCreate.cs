using System.ComponentModel.DataAnnotations;

namespace Restaurant.Contracts.Recipes.Requests
{
    public class RecipeIngredientCreate
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public Guid UserProfileId { get; set; }
    }
}

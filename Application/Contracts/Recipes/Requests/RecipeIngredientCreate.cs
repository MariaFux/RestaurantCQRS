using System.ComponentModel.DataAnnotations;

namespace Application.Contracts.Recipes.Requests
{
    public class RecipeIngredientCreate
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public Guid UserProfileId { get; set; }
    }
}

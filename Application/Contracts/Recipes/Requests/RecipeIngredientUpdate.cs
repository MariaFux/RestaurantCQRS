using System.ComponentModel.DataAnnotations;

namespace Application.Contracts.Recipes.Requests
{
    public class RecipeIngredientUpdate
    {
        [Required]
        public string Name { get; set; }
    }
}

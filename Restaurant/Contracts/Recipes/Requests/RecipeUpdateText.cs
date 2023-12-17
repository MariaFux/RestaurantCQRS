using System.ComponentModel.DataAnnotations;

namespace Restaurant.Contracts.Recipes.Requests
{
    public class RecipeUpdateText
    {
        [Required]
        public string Text { get; set; }
    }
}

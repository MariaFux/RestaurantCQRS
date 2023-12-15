using System.ComponentModel.DataAnnotations;

namespace Application.Contracts.Recipes.Requests
{
    public class RecipeUpdateText
    {
        [Required]
        public string Text { get; set; }
    }
}

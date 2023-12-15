using System.ComponentModel.DataAnnotations;

namespace Application.Contracts.Recipes.Requests
{
    public class RecipeUpdateName
    {
        [Required]
        public string Name { get; set; }
    }
}

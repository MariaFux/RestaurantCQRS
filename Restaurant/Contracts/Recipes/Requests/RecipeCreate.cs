using System.ComponentModel.DataAnnotations;

namespace Restaurant.Contracts.Recipes.Requests
{
    public class RecipeCreate
    {
        [Required]
        public Guid UserProfileId { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [StringLength(1000)]
        public string TextContent { get; set; }
    }
}

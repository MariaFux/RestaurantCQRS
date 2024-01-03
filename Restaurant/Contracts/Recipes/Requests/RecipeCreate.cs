namespace Restaurant.Contracts.Recipes.Requests
{
    public class RecipeCreate
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [StringLength(1000)]
        public string TextContent { get; set; }
    }
}

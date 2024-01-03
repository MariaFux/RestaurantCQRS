namespace Restaurant.Contracts.Recipes.Requests
{
    public class RecipeUpdateName
    {
        [Required]
        public string Name { get; set; }
    }
}

namespace Restaurant.Contracts.Recipes.Requests
{
    public class RecipeIngredientUpdate
    {
        [Required]
        public string Name { get; set; }
    }
}

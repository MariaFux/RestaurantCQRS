namespace Restaurant.Contracts.Recipes.Requests
{
    public class RecipeIngredientCreate
    {
        [Required]
        public string Name { get; set; }
    }
}

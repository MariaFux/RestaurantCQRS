namespace Restaurant.Contracts.Recipes.Responses
{
    public class RecipeIngredientResponse
    {
        public Guid IngredientId { get; set; }
        public string Name { get; set; }
        public Guid UserProfileId { get; set; }
    }
}

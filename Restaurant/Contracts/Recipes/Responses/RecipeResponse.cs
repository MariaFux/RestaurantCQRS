namespace Restaurant.Contracts.Recipes.Responses
{
    public class RecipeResponse
    {
        public Guid RecipeId { get; set; }
        public Guid UserProfileId { get; set; }
        public string Name { get; set; }
        public string TextContent { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModified { get; set; }
    }
}

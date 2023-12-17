namespace Restaurant.Contracts.Menus.Responses
{
    public class MenuRecipeResponse
    {
        public Guid MenuId { get; set; }
        public string MenuName { get; set; }
        public UserRecipe? RecipeAuthor { get; set; }
        public IEnumerable<RecipeContent?> RecipeContent { get; set; }
    }
}

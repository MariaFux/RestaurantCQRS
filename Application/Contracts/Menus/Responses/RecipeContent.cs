namespace Application.Contracts.Menus.Responses
{
    public class RecipeContent
    {
        public Guid RecipeId { get; set; }
        public string Name { get; set; }
        public string TextContent { get; set; }
        public IEnumerable<string> Ingredients { get; set; } = new List<string>();
    }
}

namespace Application.Contracts.Menus.Responses
{
    public class UserRecipe
    {
        public Guid UserProfileId { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }
    }
}

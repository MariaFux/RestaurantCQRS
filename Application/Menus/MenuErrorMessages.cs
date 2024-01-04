namespace Application.Menus
{
    public class MenuErrorMessages
    {
        public const string MenuNotFound = "No Menus found with ID {0}";
        public const string AddRecipeToMenuNotPossible = "Add recipe to menu not possible because it's not the menu owner that initiates the add";
        public const string RecipeNotFound = "No Recipes found with ID {0}";
        public const string MenuDeleteNotPossible = "Only the owner of a menu can delete it";
    }
}

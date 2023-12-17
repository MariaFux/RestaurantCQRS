using System.ComponentModel.DataAnnotations;

namespace Restaurant.Contracts.Menus.Requests
{
    public class MenuRecipeCreate
    {
        [Required]
        public Guid UserProfileId { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Application.Contracts.Menus.Requests
{
    public class MenuRecipeCreate
    {
        [Required]
        public Guid UserProfileId { get; set; }
    }
}

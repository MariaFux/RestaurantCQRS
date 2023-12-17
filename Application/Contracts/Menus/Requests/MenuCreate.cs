using System.ComponentModel.DataAnnotations;

namespace Application.Contracts.Menus.Requests
{
    public class MenuCreate
    {
        [Required]
        public Guid UserProfileId { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
    }
}

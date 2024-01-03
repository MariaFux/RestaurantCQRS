namespace Restaurant.Contracts.Menus.Requests
{
    public class MenuCreate
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
    }
}

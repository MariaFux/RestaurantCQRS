namespace Application.Contracts.Menus.Responses
{
    public class MenuResponse
    {
        public Guid MenuId { get; set; }
        public Guid UserProfileId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModified { get; set; }
    }
}

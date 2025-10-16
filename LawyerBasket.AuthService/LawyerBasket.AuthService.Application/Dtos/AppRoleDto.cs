namespace LawyerBasket.AuthService.Application.Dtos
{
    public class AppRoleDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public List<AppUserRoleDto> AppUserRole { get; set; }
    }
}

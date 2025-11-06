namespace LawyerBasket.AuthService.Application.Dtos
{
    public class AppRoleDto
    {
        public string Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public List<AppUserRoleDto>? AppUserRole { get; set; }
    }
}

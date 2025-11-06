namespace LawyerBasket.AuthService.Application.Dtos
{
    public class AppUserRoleDto
    {
        public AppUserDto? AppUserDto { get; set; }
        public string UserId { get; set; } = default!;
        public string RoleId { get; set; } = default!;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public AppRoleDto? AppRoleDto { get; set; }
    }
}

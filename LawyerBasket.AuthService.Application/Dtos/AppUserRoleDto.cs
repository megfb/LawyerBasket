namespace LawyerBasket.AuthService.Application.Dtos
{
    public class AppUserRoleDto
    {
        public AppUserDto AppUserDto { get; set; }
        public string UserId { get; set; }
        public string RoleId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public AppRoleDto AppRoleDto { get; set; }
    }
}

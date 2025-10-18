namespace LawyerBasket.AuthService.Application.Dtos
{
  public class AppUserDto
  {
    public string Id { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public DateTime? LastLoginAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public List<AppUserRoleDto> AppUserRoleDto { get; set; }
  }
}

using LawyerBasket.Shared.Common.Domain;

namespace LawyerBasket.AuthService.Domain.Entities
{
  public class AppUserRole : Entity
  {
    public AppUser? AppUser { get; set; }
    public string UserId { get; set; } = default!;
    public string RoleId { get; set; } = default!;
    public AppRole? AppRole { get; set; }
  }
}

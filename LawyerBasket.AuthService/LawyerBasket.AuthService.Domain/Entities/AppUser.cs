using LawyerBasket.Shared.Common.Domain;

namespace LawyerBasket.AuthService.Domain.Entities
{
  public class AppUser : Entity
  {
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public DateTime? LastLoginAt { get; set; }
    public List<AppUserRole>? AppUserRole { get; set; }
  }
}

using LawyerBasket.ProfileService.Domain.Entities.Common;

namespace LawyerBasket.ProfileService.Domain.Entities
{
  public class Contact:Entity
  {
    public LawyerProfile LawyerProfile { get; set; }
    public string LawyerProfileId { get; set; }
    public string PhoneNumber { get; set; } = default!;
    public string? AlternatePhoneNumber { get; set; }
    public string Email { get; set; } = default!;
    public string? AlternateEmail { get; set; }
    public string? Website { get; set; } = default!;
  }
}

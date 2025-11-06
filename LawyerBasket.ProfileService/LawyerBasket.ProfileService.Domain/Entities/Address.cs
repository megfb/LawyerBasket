using LawyerBasket.Shared.Common.Domain;

namespace LawyerBasket.ProfileService.Domain.Entities
{
  public class Address : Entity
  {
    public string UserProfileId { get; set; } = default!;
    public UserProfile? UserProfile { get; set; }
    public string AddressLine { get; set; } = default!;
    public City? City { get; set; }
    public string CityId { get; set; } = default!;
  }
}

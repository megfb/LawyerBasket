using LawyerBasket.ProfileService.Domain.Entities.Common;

namespace LawyerBasket.ProfileService.Domain.Entities
{
  public class Address : Entity
  {
    public string UserProfileId { get; set; }
    public UserProfile UserProfile { get; set; }
    public string AddressLine { get; set; }
    public City City { get; set; }
    public string CityId { get; set; }
  }
}

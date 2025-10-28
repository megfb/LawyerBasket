using LawyerBasket.Shared.Common.Domain;

namespace LawyerBasket.ProfileService.Domain.Entities
{
  public class Gender : Entity
  {
    public string Name { get; set; }
    public string Description { get; set; }
    public List<UserProfile> UserProfile { get; set; }
  }
}

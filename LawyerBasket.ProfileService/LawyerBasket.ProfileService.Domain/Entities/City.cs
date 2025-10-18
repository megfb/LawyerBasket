using LawyerBasket.ProfileService.Domain.Entities.Common;

namespace LawyerBasket.ProfileService.Domain.Entities
{
  public class City : Entity
  {
    public string Name { get; set; }
    public List<Address> Address { get; set; }
  }
}


using LawyerBasket.Shared.Common.Domain;

namespace LawyerBasket.ProfileService.Domain.Entities
{
  public class City : Entity
  {
    public string Name { get; set; }
    public List<Address> Address { get; set; }
  }
}

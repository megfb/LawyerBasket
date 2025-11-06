using LawyerBasket.Shared.Common.Domain;

namespace LawyerBasket.ProfileService.Domain.Entities
{
  public class Expertisement : Entity
  {
    public List<LawyerExpertisement>? lawyerExpertisement { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;

  }
}

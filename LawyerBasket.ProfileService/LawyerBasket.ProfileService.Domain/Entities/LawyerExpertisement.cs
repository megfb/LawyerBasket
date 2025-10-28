using LawyerBasket.Shared.Common.Domain;

namespace LawyerBasket.ProfileService.Domain.Entities
{
  public class LawyerExpertisement : Entity
  {
    public LawyerProfile LawyerProfile { get; set; }
    public string LawyerProfileId { get; set; }
    public Expertisement Expertisement { get; set; }
    public string ExpertisementId { get; set; }
  }
}

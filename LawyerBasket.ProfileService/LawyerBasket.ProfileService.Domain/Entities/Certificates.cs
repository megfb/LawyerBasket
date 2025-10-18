using LawyerBasket.ProfileService.Domain.Entities.Common;

namespace LawyerBasket.ProfileService.Domain.Entities
{
  public class Certificates : Entity
  {
    public LawyerProfile LawyerProfile { get; set; }
    public string LawyerProfileId { get; set; }
    public string Name { get; set; }
    public string Institution { get; set; }
    public DateTime DateReceived { get; set; }

  }
}

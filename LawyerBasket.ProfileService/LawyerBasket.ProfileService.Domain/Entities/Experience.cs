using LawyerBasket.ProfileService.Domain.Entities.Common;

namespace LawyerBasket.ProfileService.Domain.Entities
{
  public class Experience : Entity
  {
    public LawyerProfile LawyerProfile { get; set; }
    public string LawyerProfileId { get; set; }
    public string CompanyName { get; set; }
    public string Position { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string Description { get; set; }

  }
}

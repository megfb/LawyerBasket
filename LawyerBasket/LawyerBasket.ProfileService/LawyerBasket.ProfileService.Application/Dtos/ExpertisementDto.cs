using LawyerBasket.ProfileService.Domain.Entities;

namespace LawyerBasket.ProfileService.Application.Dtos
{
  public class ExpertisementDto
  {
    public string Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
  }
}

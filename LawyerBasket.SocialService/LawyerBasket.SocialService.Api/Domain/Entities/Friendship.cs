
using LawyerBasket.Shared.Common.Domain;

namespace LawyerBasket.SocialService.Api.Domain.Entities
{
  public class Friendship : Entity
  {
    public string UserAId { get; set; } = default!;
    public string UserBId { get; set; } = default!;
    public DateTime? EndedAt { get; set; }
    public bool IsActive { get; set; }
  }
}

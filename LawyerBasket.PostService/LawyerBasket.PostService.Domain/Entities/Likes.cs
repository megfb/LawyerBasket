using LawyerBasket.Shared.Common.Domain;

namespace LawyerBasket.PostService.Domain.Entities
{
  public class Likes : Entity
  {
    public string UserId { get; set; }
    public string PostId { get; set; }
  }
}

using LawyerBasket.Shared.Common.Domain;

namespace LawyerBasket.PostService.Domain.Entities
{
  public class Comment : Entity
  {
    public string UserId { get; set; } = default!;
    public string PostId { get; set; } = default!;
    public string Text { get; set; } = default!;

  }
}

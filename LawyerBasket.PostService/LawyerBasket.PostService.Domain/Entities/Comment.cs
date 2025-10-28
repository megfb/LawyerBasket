using LawyerBasket.Shared.Common.Domain;

namespace LawyerBasket.PostService.Domain.Entities
{
  public class Comment : Entity
  {
    public string UserId { get; set; }
    public string PostId { get; set; }
    public string Text { get; set; }

  }
}

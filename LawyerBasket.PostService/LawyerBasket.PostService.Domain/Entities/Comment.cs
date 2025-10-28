using LawyerBasket.PostService.Domain.Entities.Common;

namespace LawyerBasket.PostService.Domain.Entities
{
  public class Comment : Entity
  {
    public string UserId { get; set; }
    public string PostId { get; set; }
    public string Text { get; set; }

  }
}

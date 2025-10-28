using LawyerBasket.PostService.Domain.Entities.Common;

namespace LawyerBasket.PostService.Domain.Entities
{
  public class Post : Entity
  {
    public string UserId { get; set; }
    public string Content { get; set; }
    public List<string>? Likes { get; set; }
    public List<Comment>? Comments { get; set; }

  }
}

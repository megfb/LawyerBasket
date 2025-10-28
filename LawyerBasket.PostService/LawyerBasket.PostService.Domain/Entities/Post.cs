using LawyerBasket.Shared.Common.Domain;

namespace LawyerBasket.PostService.Domain.Entities
{
  public class Post : Entity
  {
    public string UserId { get; set; }
    public string Content { get; set; }
    public List<Likes>? Likes { get; set; } = new List<Likes>();
    public List<Comment>? Comments { get; set; } = new List<Comment>();

  }
}

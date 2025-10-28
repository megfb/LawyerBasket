namespace LawyerBasket.PostService.Application.Dtos
{
  public class PostDto
  {
    public string Id { get; set; }
    public string UserId { get; set; }
    public string Content { get; set; }
    public List<string>? Likes { get; set; }
    public List<CommentDto>? Comments { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
  }
}

namespace LawyerBasket.PostService.Domain.Entities.Common
{
  public class Entity : IEntity
  {
    public string Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
  }
}

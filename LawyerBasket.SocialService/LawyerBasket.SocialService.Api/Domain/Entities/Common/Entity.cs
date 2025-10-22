namespace LawyerBasket.SocialService.Api.Domain.Entities.Common
{
  public abstract class Entity : IEntity
  {
    public string Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
  }
}

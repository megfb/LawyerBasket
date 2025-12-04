namespace LawyerBasket.Shared.Messaging.Abstractions
{
  public abstract class IIntegrationEvent : IIIntegrationEvent
  {
    public string Id { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
  }

  public interface IIIntegrationEvent
  {
  }
}












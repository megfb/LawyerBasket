namespace LawyerBasket.Shared.Messaging.Abstractions
{
  public abstract class IIntegrationEvent : IIIntegrationEvent
  {
    public string Id { get; set; }
    public DateTime CreatedAt { get; set; }
  }

  public interface IIIntegrationEvent
  {
  }
}












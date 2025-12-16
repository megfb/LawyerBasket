namespace LawyerBasket.Shared.Messaging.MassTransit
{
  public class RabbitMqSettings
  {
    public static string Host => Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production" 
      ? "rabbitmq://rabbitmq"  // Docker: use container name
      : "rabbitmq://localhost"; // Development: use localhost
    
    public const string Username = "guest";
    public const string Password = "guest";
  }
}

using LawyerBasket.Shared.Messaging;
using LawyerBasket.Shared.Messaging.Abstractions;
using LawyerBasket.Shared.Messaging.MassTransit;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

public static class MassTransitExtensions
{
  // Publisher Kayıt method
  public static IServiceCollection AddRabbitMqPublisher<TEvent>(
      this IServiceCollection services,
      string exchangeName)
      where TEvent : class
  {
    services.AddSingleton<IMassTransitFeature>(new PublisherFeature<TEvent>(exchangeName));
    return services;
  }

  // Consumer kayıt eden method
  public static IServiceCollection AddRabbitMqConsumer<TConsumer>(
      this IServiceCollection services,
      string queueName,
      string routingKey,
      string exchangeName)
      where TConsumer : class, IConsumer
  {
    services.AddSingleton<IMassTransitFeature>(new ConsumerFeature<TConsumer>(queueName, routingKey, exchangeName));
    return services;
  }

  // Her şeyi birleştirip başlatan method
  public static IServiceCollection AddRabbitMqInfrastructure(this IServiceCollection services)
  {
    services.AddMassTransit(x =>
    {

      var serviceProvider = services.BuildServiceProvider();
      var features = serviceProvider.GetServices<IMassTransitFeature>();

      // Tüm consumer'ları AddConsumer ile ekle
      foreach (var feature in features)
      {
        feature.Configure(x);
      }

      x.UsingRabbitMq((context, cfg) =>
      {
        
        // Ortak host ayarları
        cfg.Host(RabbitMqSettings.Host, h =>
        {
          h.Username(RabbitMqSettings.Username);
          h.Password(RabbitMqSettings.Password);
        });

        // Tüm publisher ve consumer endpoint ayarlarını uygula
        foreach (var feature in features)
        {
          feature.ConfigureBus(context, cfg);
        }
      });
    });

    return services;
  }
}

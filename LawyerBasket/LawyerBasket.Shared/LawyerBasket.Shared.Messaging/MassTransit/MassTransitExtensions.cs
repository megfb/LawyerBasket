using LawyerBasket.Shared.Messaging.Abstractions;
using MassTransit;
using MassTransit.RabbitMqTransport;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace LawyerBasket.Shared.Messaging.MassTransit
{
  public static class MassTransitExtensions
  {
    public static IServiceCollection AddRabbitMqPublisher<TEvent>(this IServiceCollection services, string exchangeName) where TEvent : class
    {
      services.AddMassTransit(x =>
      {
        x.UsingRabbitMq((context, cfg) =>
        {
          // Ortak Host Ayarları
          cfg.Host("rabbitmq://localhost", h =>
          {
            h.Username("guest");
            h.Password("guest");
          });

          // --- TOPOLOGY AYARLARI (Mesajlar nereye gidecek?) ---

          // 1. Her iki mesaj tipi de AYNI Exchange'e ("micros.events") gitsin.
          cfg.Message<TEvent>(m => m.SetEntityName(exchangeName));

          // 2. Bu Exchange'in tipi "Topic" olsun.
          cfg.Publish(typeof(TEvent), p => p.ExchangeType = RabbitMQ.Client.ExchangeType.Topic);
        });
      });

      return services;
    }

    // --- B ve C SERVİSLERİ (CONSUMER) İÇİN ---
    // Generic <TConsumer> yapısı sayesinde her servis kendi Consumer sınıfını verebilir.
    public static IServiceCollection AddRabbitMqConsumer<TConsumer>(
        this IServiceCollection services,
        string queueName,
        string routingKey, string exchangeName)
        where TConsumer : class, IConsumer
    {
      services.AddMassTransit(x =>
      {
        // Gelen Consumer tipini sisteme ekle (Örn: MessageBConsumer)
        x.AddConsumer<TConsumer>();

        x.UsingRabbitMq((context, cfg) =>
        {
          cfg.Host("rabbitmq://localhost", h =>
          {
            h.Username("guest");
            h.Password("guest");
          });

          cfg.ReceiveEndpoint(queueName, e =>
          {
            // Otomatik oluşturmayı kapat (MassTransit varsayılanını ezmek için)
            e.ConfigureConsumeTopology = false;
            e.ConfigureConsumer<TConsumer>(context);

            // EXPLICIT BINDING (Açık Bağlama)
            // "micros.events" exchange'ine bağlan, 
            // SADECE "route.service.b" etiketli mesajları al.
            e.Bind(exchangeName, s =>
            {
              s.ExchangeType = RabbitMQ.Client.ExchangeType.Topic;
              s.RoutingKey = routingKey;
            });
          });
        });
      });

      return services;
    }
  }
}


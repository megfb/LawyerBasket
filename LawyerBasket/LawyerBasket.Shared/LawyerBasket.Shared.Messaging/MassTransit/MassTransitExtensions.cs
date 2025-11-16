using LawyerBasket.Shared.Messaging.Abstractions;
using MassTransit;
using MassTransit.RabbitMqTransport;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace LawyerBasket.Shared.Messaging.MassTransit
{
    /// <summary>
    /// MassTransit yapılandırması için extension metodlar.
    /// Bu extension'lar ile MassTransit ve RabbitMQ merkezi olarak yapılandırılır.
    /// </summary>
    public static class MassTransitExtensions
    {
        /// <summary>
        /// MassTransit ve RabbitMQ'yu yapılandırır.
        /// Topic exchange kullanır ve consumer'ları otomatik olarak tarar.
        /// </summary>
        /// <param name="services">Service collection</param>
        /// <param name="configuration">Configuration</param>
        /// <param name="assemblies">Consumer'ların bulunacağı assembly'ler. Eğer belirtilmezse, çağıran assembly kullanılır.</param>
        /// <returns>IServiceCollection</returns>
        public static IServiceCollection AddMassTransitWithRabbitMq(
            this IServiceCollection services,
            IConfiguration configuration,
            params Assembly[] assemblies)
        {
            // RabbitMQ ayarlarını yükle
            var rabbitMqSettings = configuration.GetSection(RabbitMqSettings.SectionName).Get<RabbitMqSettings>()
                ?? new RabbitMqSettings();

            // RabbitMQ ayarlarını DI container'a ekle
            services.Configure<RabbitMqSettings>(configuration.GetSection(RabbitMqSettings.SectionName));

            // Eğer assembly belirtilmemişse, çağıran assembly'yi kullan
            if (assemblies == null || assemblies.Length == 0)
            {
                assemblies = new[] { Assembly.GetCallingAssembly() };
            }

            // MassTransit yapılandırması
            services.AddMassTransit(x =>
            {
                // Tüm belirtilen assembly'lerdeki consumer'ları otomatik olarak tara ve kaydet
                foreach (var assembly in assemblies)
                {
                    x.AddConsumers(assembly);
                }

                // RabbitMQ bus yapılandırması
                x.UsingRabbitMq((context, cfg) =>
                {
                    // RabbitMQ bağlantı ayarları
                    // MassTransit 8.x'te Host metodu için connection string veya host, port parametreleri kullanılır
                    // VirtualHost'u connection string içinde veya host metoduna parametre olarak geçirebiliriz
                    var virtualHost = string.IsNullOrEmpty(rabbitMqSettings.VirtualHost) || rabbitMqSettings.VirtualHost == "/"
                        ? "/"
                        : rabbitMqSettings.VirtualHost.TrimStart('/');

                    // Host metodunu host, port ve virtualHost ile çağır
                    cfg.Host(rabbitMqSettings.Host, (ushort)rabbitMqSettings.Port, virtualHost, h =>
                    {
                        h.Username(rabbitMqSettings.Username);
                        h.Password(rabbitMqSettings.Password);
                        h.Heartbeat(TimeSpan.FromSeconds(30));
                    });

                    // Topic exchange kullanımı için publish topology ayarı
                    // MassTransit 8.x'te tüm message type'lar için topic exchange kullan
                    // Bu, tüm event'lerin topic exchange üzerinden yayınlanmasını sağlar
                    cfg.Publish<object>(p =>
                    {
                        p.ExchangeType = "topic";
                    });

                    // Retry policy yapılandırması
                    cfg.UseMessageRetry(r =>
                    {
                        r.Interval(rabbitMqSettings.RetryCount, TimeSpan.FromSeconds(rabbitMqSettings.RetryInterval));
                        r.Handle<Exception>();
                    });

                    // Consumer endpoint'lerini otomatik yapılandır
                    // Her consumer için ayrı queue oluşturulur
                    // Topic exchange kullanımı için endpoint'ler otomatik olarak yapılandırılır
                    cfg.ConfigureEndpoints(context, new DefaultEndpointNameFormatter(false));
                });
            });

            // MassTransit hosted service'i ekle (bus'ı başlatır)
            services.AddMassTransitHostedService();

            return services;
        }

        /// <summary>
        /// MassTransit ve RabbitMQ'yu yapılandırır (sadece belirtilen assembly için).
        /// </summary>
        /// <param name="services">Service collection</param>
        /// <param name="configuration">Configuration</param>
        /// <param name="assembly">Consumer'ların bulunacağı assembly</param>
        /// <returns>IServiceCollection</returns>
        public static IServiceCollection AddMassTransitWithRabbitMq(
            this IServiceCollection services,
            IConfiguration configuration,
            Assembly assembly)
        {
            return services.AddMassTransitWithRabbitMq(configuration, new[] { assembly });
        }

        /// <summary>
        /// IPublishEndpoint'i kullanarak event publish etmek için extension metod.
        /// Topic exchange kullanır.
        /// </summary>
        /// <typeparam name="T">IIntegrationEvent türünde bir event</typeparam>
        /// <param name="publishEndpoint">Publish endpoint</param>
        /// <param name="event">Publish edilecek event</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Task</returns>
        public static async Task PublishEventAsync<T>(
            this IPublishEndpoint publishEndpoint,
            T @event,
            CancellationToken cancellationToken = default) where T : class, IIIntegrationEvent
        {
            await publishEndpoint.Publish(@event, cancellationToken);
        }
    }
}


using LawyerBasket.ProfileService.Application.Contracts.Data;
using LawyerBasket.ProfileService.Domain.Entities;
using LawyerBasket.Shared.Common.Domain;
using LawyerBasket.Shared.Messaging.Events;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace LawyerBasket.ProfileService.Worker.BackgroundServices
{
  public class OutboxPublisherWorker : BackgroundService
  {
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<OutboxPublisherWorker> _logger;

    public OutboxPublisherWorker(IServiceProvider serviceProvider, ILogger<OutboxPublisherWorker> logger)
    {
      _serviceProvider = serviceProvider;
      _logger = logger;

    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
      _logger.LogInformation("Outbox Worker Service çalışıyor...");

      while (!stoppingToken.IsCancellationRequested)
      {
        try
        {
          await ProcessOutboxMessagesAsync(stoppingToken);
        }
        catch (Exception ex)
        {
          _logger.LogError(ex, "Outbox işleme sırasında hata oluştu.");
        }

        // Veritabanını kilitlememek için bekleme süresi
        await Task.Delay(2000, stoppingToken);
      }
    }

    private async Task ProcessOutboxMessagesAsync(CancellationToken stoppingToken)
    {
      using (var scope = _serviceProvider.CreateScope())
      {
        // Scoped servisleri alıyoruz
        var repository = scope.ServiceProvider.GetRequiredService<IOutboxMessageRepository>();
        var publishEndpoint = scope.ServiceProvider.GetRequiredService<IPublishEndpoint>();
        var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

        // 1. Bekleyen Mesajları Getir (Repository kodunuzu çağırdık)
        var messages = await repository.GetPendingMessagesAsync(stoppingToken);

        if (messages == null || !messages.Any()) return;

        foreach (var message in messages)
        {
          try
          {
            var eventsAssembly = typeof(UserProfileCreatedEvent).Assembly;
            // 2. String olan Type bilgisini Gerçek C# Tipine çevir
            // Veritabanında AssemblyQualifiedName tuttuğunuz için bu method tam olarak o tipi bulur.
            var eventType = eventsAssembly.GetTypes()
                .FirstOrDefault(x => x.Name == message.Type);
            if (eventType == null)
            {
              throw new Exception($"Type çözümlenemedi: {message.Type}");
            }

            // 3. EVENT TİPİNE GÖRE İŞLEM YAP (Switch - Case Pattern)
            // Routing Key veya özel mapping ihtiyaçlarınız için burası en esnek yerdir.

            if (eventType == typeof(UserProfileCreatedEvent))
            {
              // A) Payload'ı Deserilaze Et
              // Not: JSON property isimleri ile Class property isimleri eşleşmeli!
              var eventData = JsonSerializer.Deserialize<UserProfileCreatedEvent>(message.Payload);

              if (eventData != null)
              {
                // B) RabbitMQ'ya Publish Et (Routing Key ile)
                await publishEndpoint.Publish<UserProfileCreatedEvent>(eventData, context =>
                {
                  // Sizin istediğiniz Routing Key ayarı
                  // Genelde AggregateId (UserId) veya UserType kullanılır.
                  context.SetRoutingKey(message.Type);
                }, stoppingToken);

                _logger.LogInformation($"UserProfileCreatedEvent gönderildi. UserID: {message.AggregateId}");
              }
            }
            else if (eventType == typeof(UserProfileUpdatedEvent))
            {
              // A) Payload'ı Update sınıfına göre deserialize et
              var eventData = JsonSerializer.Deserialize<UserProfileUpdatedEvent>(message.Payload);

              if (eventData != null)
              {
                // B) RabbitMQ'ya Publish Et
                await publishEndpoint.Publish<UserProfileUpdatedEvent>(eventData, context =>
                {
                  // Update için de routing key ayarı (Create ile aynı mantıkta)
                  context.SetRoutingKey(message.Type);
                }, stoppingToken);

                _logger.LogInformation($"Update Event gönderildi: {message.AggregateId}");
              }
            }
            // Başka eventleriniz varsa buraya 'else if' olarak ekleyebilirsiniz.
            // else if (eventType == typeof(UserUpdatedEvent)) { ... }
            else
            {
              _logger.LogWarning($"İşlenmeyen Event Tipi: {eventType.Name}");
            }

            // 4. Başarılı Durum Güncellemesi
            message.Status = Status.Processed;
            message.ProcessedAt = DateTime.UtcNow;
            message.Error = null;
          }
          catch (Exception ex)
          {
            // 5. Hata Yönetimi
            message.Status = Status.Failed;
            message.Error = ex.ToString();
            _logger.LogError(ex, $"Mesaj ID: {message.Id} işlenirken hata oluştu.");
          }

          // 6. Veritabanını Güncelle
          repository.Update(message);
          await unitOfWork.SaveChangesAsync();

        }
      }
    }
  }
}


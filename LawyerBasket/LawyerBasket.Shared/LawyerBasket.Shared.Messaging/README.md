# LawyerBasket.Shared.Messaging

Bu proje, LawyerBasket mikroservis mimarisinde MassTransit + RabbitMQ kullanarak **Topic Exchange** tabanlı event-driven communication sağlar.

## 📋 İçindekiler

- [Özellikler](#özellikler)
- [Proje Yapısı](#proje-yapısı)
- [Kurulum](#kurulum)
- [Kullanım](#kullanım)
- [Event Oluşturma](#event-oluşturma)
- [Consumer Oluşturma](#consumer-oluşturma)
- [Yapılandırma](#yapılandırma)
- [Örnekler](#örnekler)

## ✨ Özellikler

- ✅ **Topic Exchange**: Tüm event'ler topic exchange üzerinden yayınlanır
- ✅ **Otomatik Consumer Tarama**: Assembly scanning ile consumer'lar otomatik bulunur
- ✅ **Merkezi Yapılandırma**: Tek bir extension metod ile tüm yapılandırma
- ✅ **Retry Policy**: Hata durumunda otomatik retry mekanizması
- ✅ **Type-Safe Events**: IIntegrationEvent interface ile tip güvenliği
- ✅ **Clean Architecture**: Temiz ve sürdürülebilir mimari

## 📁 Proje Yapısı

```
LawyerBasket.Shared.Messaging
├── Abstractions
│   └── IIntegrationEvent.cs          # Tüm event'lerin türeyeceği base interface
├── Events
│   └── UserCreatedEvent.cs           # Örnek integration event
├── Consumers
│   └── UserCreatedEventConsumer.cs   # Örnek consumer
├── MassTransit
│   ├── MassTransitExtensions.cs      # MassTransit yapılandırma extension'ları
│   └── RabbitMqSettings.cs           # RabbitMQ ayar sınıfı
└── README.md
```

## 🚀 Kurulum

### 1. NuGet Paketleri

Proje aşağıdaki NuGet paketlerini içerir:
- `MassTransit` (8.3.0)
- `MassTransit.RabbitMQ` (8.3.0)
- `Microsoft.Extensions.Configuration.Abstractions` (9.0.10)
- `Microsoft.Extensions.DependencyInjection.Abstractions` (9.0.10)
- `Microsoft.Extensions.Logging.Abstractions` (9.0.10)

### 2. Proje Referansı

Mikroservisinizin `.csproj` dosyasına referans ekleyin:

```xml
<ItemGroup>
  <ProjectReference Include="..\..\LawyerBasket.Shared\LawyerBasket.Shared.Messaging\LawyerBasket.Shared.Messaging.csproj" />
</ItemGroup>
```

## 📖 Kullanım

### Program.cs'de Yapılandırma

```csharp
using LawyerBasket.Shared.Messaging.MassTransit;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// MassTransit ve RabbitMQ yapılandırması
builder.Services.AddMassTransitWithRabbitMq(
    builder.Configuration,
    Assembly.GetExecutingAssembly() // Consumer'ların bulunacağı assembly
);

var app = builder.Build();
```

### appsettings.json Yapılandırması

```json
{
  "RabbitMq": {
    "Host": "localhost",
    "Port": 5672,
    "Username": "guest",
    "Password": "guest",
    "VirtualHost": "/",
    "RetryCount": 3,
    "RetryInterval": 5,
    "ConnectionTimeout": 30
  }
}
```

## 🎯 Event Oluşturma

Yeni bir integration event oluşturmak için:

1. `IIntegrationEvent` interface'inden türetin
2. `Events` klasörüne ekleyin

```csharp
using LawyerBasket.Shared.Messaging.Abstractions;

namespace YourService.Events
{
    public class OrderCreatedEvent : IIntegrationEvent
    {
        public Guid Id { get; set; }
        public DateTime OccurredOn { get; set; }
        
        public string OrderId { get; set; } = default!;
        public string CustomerId { get; set; } = default!;
        public decimal TotalAmount { get; set; }

        public OrderCreatedEvent()
        {
            Id = Guid.NewGuid();
            OccurredOn = DateTime.UtcNow;
        }
    }
}
```

## 🔄 Consumer Oluşturma

Event'i consume etmek için:

1. `IConsumer<T>` interface'ini implement edin
2. `Consumers` klasörüne ekleyin
3. Assembly scanning otomatik olarak bulacaktır

```csharp
using LawyerBasket.Shared.Messaging.Events;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace YourService.Consumers
{
    public class OrderCreatedEventConsumer : IConsumer<OrderCreatedEvent>
    {
        private readonly ILogger<OrderCreatedEventConsumer> _logger;

        public OrderCreatedEventConsumer(ILogger<OrderCreatedEventConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {
            var @event = context.Message;
            
            _logger.LogInformation("Order created: {OrderId}", @event.OrderId);
            
            // İş mantığınızı buraya ekleyin
            await ProcessOrderAsync(@event);
        }

        private async Task ProcessOrderAsync(OrderCreatedEvent @event)
        {
            // İşlemler...
            await Task.CompletedTask;
        }
    }
}
```

## 📤 Event Publish Etme

Event publish etmek için `IPublishEndpoint` kullanın:

```csharp
using MassTransit;
using LawyerBasket.Shared.Messaging.Events;

public class YourService
{
    private readonly IPublishEndpoint _publishEndpoint;

    public YourService(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    public async Task CreateUserAsync(string userId, string email, string firstName, string lastName)
    {
        // Kullanıcı oluşturma işlemi...
        
        // Event publish et
        var @event = new UserCreatedEvent(userId, email, firstName, lastName, DateTime.UtcNow);
        await _publishEndpoint.PublishEventAsync(@event);
    }
}
```

## ⚙️ Yapılandırma Detayları

### RabbitMqSettings

| Özellik | Açıklama | Varsayılan |
|---------|----------|------------|
| `Host` | RabbitMQ sunucu adresi | `localhost` |
| `Port` | RabbitMQ port numarası | `5672` |
| `Username` | RabbitMQ kullanıcı adı | `guest` |
| `Password` | RabbitMQ şifresi | `guest` |
| `VirtualHost` | RabbitMQ virtual host | `/` |
| `RetryCount` | Retry sayısı | `3` |
| `RetryInterval` | Retry aralığı (saniye) | `5` |
| `ConnectionTimeout` | Bağlantı timeout (saniye) | `30` |

### Topic Exchange

- Tüm event'ler **topic exchange** üzerinden yayınlanır
- Exchange türü otomatik olarak `topic` olarak ayarlanır
- Her consumer için ayrı queue oluşturulur
- Routing key, event adına göre otomatik oluşturulur

## 📝 Örnekler

### Örnek 1: Basit Event Publish

```csharp
public class UserService
{
    private readonly IPublishEndpoint _publishEndpoint;

    public UserService(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    public async Task CreateUserAsync(CreateUserRequest request)
    {
        // Kullanıcı oluştur
        var user = new User { /* ... */ };
        
        // Event publish et
        var @event = new UserCreatedEvent(
            user.Id.ToString(),
            user.Email,
            user.FirstName,
            user.LastName,
            DateTime.UtcNow
        );
        
        await _publishEndpoint.PublishEventAsync(@event);
    }
}
```

### Örnek 2: Multiple Assembly Consumer Tarama

```csharp
builder.Services.AddMassTransitWithRabbitMq(
    builder.Configuration,
    Assembly.GetExecutingAssembly(),
    typeof(UserCreatedEventConsumer).Assembly,
    typeof(OrderCreatedEventConsumer).Assembly
);
```

### Örnek 3: Controller'dan Event Publish

```csharp
[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IPublishEndpoint _publishEndpoint;

    public UsersController(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
    {
        // Kullanıcı oluşturma işlemi...
        
        var @event = new UserCreatedEvent(/* ... */);
        await _publishEndpoint.PublishEventAsync(@event);
        
        return Ok();
    }
}
```

## 🔍 Debugging

### RabbitMQ Management UI

RabbitMQ Management UI'ı kullanarak queue'ları ve exchange'leri görüntüleyebilirsiniz:

1. RabbitMQ Management UI'ı başlatın: `http://localhost:15672`
2. Default credentials: `guest` / `guest`
3. Exchanges ve Queues sekmesinden durumu kontrol edin

### Logging

MassTransit otomatik olarak loglama yapar. Log seviyesini `appsettings.json`'da ayarlayın:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "MassTransit": "Debug"
    }
  }
}
```

## 🛠️ Best Practices

1. **Event Naming**: Event isimlerini açıklayıcı ve tutarlı tutun (örn: `UserCreatedEvent`, `OrderCancelledEvent`)
2. **Idempotency**: Consumer'larda idempotent işlemler yapın
3. **Error Handling**: Retry policy'yi doğru yapılandırın
4. **Event Versioning**: Event yapısı değiştiğinde versioning kullanın
5. **Async Operations**: Consumer'larda async işlemler kullanın

## 📚 Kaynaklar

- [MassTransit Documentation](https://masstransit.io/documentation)
- [RabbitMQ Documentation](https://www.rabbitmq.com/documentation.html)
- [Topic Exchange Pattern](https://www.rabbitmq.com/tutorials/tutorial-five-dotnet.html)

## 📄 Lisans

Bu proje LawyerBasket çözümünün bir parçasıdır.


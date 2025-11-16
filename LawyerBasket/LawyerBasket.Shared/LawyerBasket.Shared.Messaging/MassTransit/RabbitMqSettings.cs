namespace LawyerBasket.Shared.Messaging.MassTransit
{
    /// <summary>
    /// RabbitMQ bağlantı ayarlarını tutan sınıf.
    /// appsettings.json'dan yapılandırılabilir.
    /// </summary>
    public class RabbitMqSettings
    {
        public const string SectionName = "RabbitMq";

        /// <summary>
        /// RabbitMQ sunucu adresi (örn: localhost, rabbitmq.example.com)
        /// </summary>
        public string Host { get; set; } = "localhost";

        /// <summary>
        /// RabbitMQ port numarası (varsayılan: 5672)
        /// </summary>
        public int Port { get; set; } = 5672;

        /// <summary>
        /// RabbitMQ kullanıcı adı
        /// </summary>
        public string Username { get; set; } = "guest";

        /// <summary>
        /// RabbitMQ şifresi
        /// </summary>
        public string Password { get; set; } = "guest";

        /// <summary>
        /// RabbitMQ virtual host (varsayılan: /)
        /// </summary>
        public string VirtualHost { get; set; } = "/";

        /// <summary>
        /// Retry sayısı (varsayılan: 3)
        /// </summary>
        public int RetryCount { get; set; } = 3;

        /// <summary>
        /// Retry interval (saniye cinsinden, varsayılan: 5)
        /// </summary>
        public int RetryInterval { get; set; } = 5;

        /// <summary>
        /// Connection timeout (saniye cinsinden, varsayılan: 30)
        /// </summary>
        public int ConnectionTimeout { get; set; } = 30;

        /// <summary>
        /// RabbitMQ bağlantı string'ini döndürür.
        /// Format: amqp://username:password@host:port/virtualHost
        /// </summary>
        public string GetConnectionString()
        {
            return $"amqp://{Username}:{Password}@{Host}:{Port}{VirtualHost}";
        }
    }
}


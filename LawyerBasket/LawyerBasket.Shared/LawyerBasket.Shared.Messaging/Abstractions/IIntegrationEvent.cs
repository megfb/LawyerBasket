namespace LawyerBasket.Shared.Messaging.Abstractions
{
    /// <summary>
    /// Tüm integration event'lerin türeyeceği temel interface.
    /// Integration event'ler mikroservisler arası asenkron iletişim için kullanılır.
    /// </summary>
    public abstract class IIntegrationEvent:IIIntegrationEvent
    {
        /// <summary>
        /// Event'in benzersiz kimliği
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Event'in oluşturulma zamanı (UTC)
        /// </summary>
        public DateTime CreatedAt { get; set; }
    }
}


using LawyerBasket.Shared.Messaging.Abstractions;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerBasket.Shared.Messaging.MassTransit
{
    public class PublisherFeature<TEvent> : IMassTransitFeature where TEvent : class
    {
        private readonly string _exchangeName;

        public PublisherFeature(string exchangeName)
        {
            _exchangeName = exchangeName;
        }

        public void Configure(IBusRegistrationConfigurator configurator)
        {
            // Publisher için burada özel bir ekleme yapmaya gerek yok
            // Ama gerekirse Future/Saga vb. burada eklenebilir.
        }

        public void ConfigureBus(IBusRegistrationContext context, IRabbitMqBusFactoryConfigurator cfg)
        {
            // Mesaj topolojisi ayarları
            cfg.Message<TEvent>(m => m.SetEntityName(_exchangeName));
            cfg.Publish(typeof(TEvent), p => p.ExchangeType = RabbitMQ.Client.ExchangeType.Topic);
        }
    }
}

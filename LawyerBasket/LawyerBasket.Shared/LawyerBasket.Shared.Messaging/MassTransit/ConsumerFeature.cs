using LawyerBasket.Shared.Messaging.Abstractions;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerBasket.Shared.Messaging.MassTransit
{
    public class ConsumerFeature<TConsumer> : IMassTransitFeature
        where TConsumer : class, IConsumer
    {
        private readonly string _queueName;
        private readonly string _routingKey;
        private readonly string _exchangeName;

        public ConsumerFeature(string queueName, string routingKey, string exchangeName)
        {
            _queueName = queueName;
            _routingKey = routingKey;
            _exchangeName = exchangeName;
        }

        public void Configure(IBusRegistrationConfigurator configurator)
        {
            // Consumer'ı DI container'a tanıt
            configurator.AddConsumer<TConsumer>();
        }

        public void ConfigureBus(IBusRegistrationContext context, IRabbitMqBusFactoryConfigurator cfg)
        {
            // Receive Endpoint ayarları
            cfg.ReceiveEndpoint(_queueName, e =>
            {
                e.ConfigureConsumeTopology = false;
                e.ConfigureConsumer<TConsumer>(context);

                e.Bind(_exchangeName, s =>
                {
                    s.ExchangeType = RabbitMQ.Client.ExchangeType.Topic;
                    s.RoutingKey = _routingKey;
                });
            });
        }
    }
}

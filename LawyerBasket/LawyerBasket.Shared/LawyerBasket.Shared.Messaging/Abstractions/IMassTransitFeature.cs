using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerBasket.Shared.Messaging.Abstractions
{
    public interface IMassTransitFeature
    {
        // MassTransit ilk yapılandırma aşaması (AddConsumer vb.)
        void Configure(IBusRegistrationConfigurator configurator);

        // RabbitMQ Bus ayağa kalkarken yapılacaklar (Endpoint, Topology vb.)
        void ConfigureBus(IBusRegistrationContext context, IRabbitMqBusFactoryConfigurator cfg);
    }
}

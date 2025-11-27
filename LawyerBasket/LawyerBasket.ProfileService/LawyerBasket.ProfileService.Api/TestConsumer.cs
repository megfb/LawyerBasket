using LawyerBasket.Shared.Messaging.Events;
using MassTransit;

namespace LawyerBasket.ProfileService.Api
{
    public class TestConsumer : IConsumer<TestEvent>
    {
        public Task Consume(ConsumeContext<TestEvent> context)
        {
            Console.WriteLine($"{context.Message.Mesaj}");
            return Task.CompletedTask;
        }
    }
}

using MyBank.Domain.Shared.Events;

namespace MyBank.Infrastructure.Events
{
    public class RabbitMQEventProducerFactory : IEventProducerFactory
    {
        public IEventProducer Create(string topicName)
        {
            return new RabbitMQEventProducer(topicName);
        }
    }
}

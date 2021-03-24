using System.Threading.Tasks;

namespace MyBank.Domain.Shared.Events
{
    public class DummyEventProducerFactory : IEventProducerFactory
    {
        public IEventProducer Create(string topicName)
        {
            return new DummyEventProducer();
        }
    }
}

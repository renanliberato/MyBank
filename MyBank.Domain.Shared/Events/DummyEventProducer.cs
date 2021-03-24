using System.Threading.Tasks;

namespace MyBank.Domain.Shared.Events
{
    public class DummyEventProducer : IEventProducer
    {
        public void Dispose()
        {
        }

        public Task Produce(IEvent theEvent)
        {
            return Task.CompletedTask;
        }
    }
}

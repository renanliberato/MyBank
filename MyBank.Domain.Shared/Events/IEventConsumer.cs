using System.Threading.Tasks;

namespace MyBank.Domain.Shared.Events
{
    public interface IEventConsumer
    {
        Task Consume(IEvent theEvent);
    }
}

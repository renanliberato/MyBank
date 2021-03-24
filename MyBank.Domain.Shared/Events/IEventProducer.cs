using System;
using System.Threading.Tasks;

namespace MyBank.Domain.Shared.Events
{
    public interface IEventProducer : IDisposable
    {
        Task Produce(IEvent theEvent);
    }
}

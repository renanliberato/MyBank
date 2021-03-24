using System;

namespace MyBank.Domain.Shared.Events
{
    public interface IEventProducerFactory
    {
        IEventProducer Create(string topicName);
    }
}

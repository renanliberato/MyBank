using MyBank.Domain.Shared.Events;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;
using System.Threading.Tasks;

namespace MyBank.Infrastructure.Events
{
    public class RabbitMQEventProducer : IEventProducer
    {
        private readonly IConnection connection;
        private readonly IModel channel;

        public RabbitMQEventProducer()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            this.connection = factory.CreateConnection();
            this.channel = connection.CreateModel();

            channel.QueueDeclare(queue: "hello",
                            durable: false,
                            exclusive: false,
                            autoDelete: false,
                            arguments: null);
        }

        public void Dispose()
        {
            this.channel.Dispose();
            this.connection.Dispose();
        }

        public Task Produce(IEvent theEvent)
        {
            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(theEvent));

            channel.BasicPublish(exchange: "",
                                 routingKey: "hello",
                                 basicProperties: null,
                                 body: body);

            return Task.CompletedTask;
        }
    }
}

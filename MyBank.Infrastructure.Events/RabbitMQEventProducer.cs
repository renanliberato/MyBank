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
        private readonly string topicName;

        public RabbitMQEventProducer(string topicName)
        {
            this.topicName = topicName;
            var factory = new ConnectionFactory() { HostName = "localhost" };
            this.connection = factory.CreateConnection();
            this.channel = connection.CreateModel();

            channel.QueueDeclare(queue: topicName,
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
                                 routingKey: this.topicName,
                                 basicProperties: null,
                                 body: body);

            return Task.CompletedTask;
        }
    }
}

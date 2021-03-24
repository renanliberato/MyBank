using Moq;
using MyBank.Clients.Domain.Commands;
using MyBank.Clients.Domain.Repositories;
using MyBank.Clients.Domain.Services;
using MyBank.Domain.Shared.Events;
using MyBank.Domain.Shared.ValueObjects;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MyBank.Clients.Domain.Tests.Services
{
    public class ClientServiceTests
    {
        [Fact]
        public async Task Register_CreatesAndPersistsANewClient()
        {
            var repository = new Mock<IClientRepository>();
            var eventProducer = new Mock<IEventProducer>();
            var eventProducerFactory = new Mock<IEventProducerFactory>();
            eventProducerFactory.Setup((obj) => obj.Create("client_removed")).Returns(eventProducer.Object);
            var command = new BecomeClient
            {
                Name = "Renan"
            };
            var service = new ClientService(repository.Object, eventProducerFactory.Object);

            var client = await service.Register(command);

            Assert.Equal(command.Name, client.Name.Name);
            repository.Verify(obj => obj.Add(It.Is<Client>(r => r.Name.Name == command.Name)), Times.Once);
            repository.Verify(obj => obj.Save(), Times.Once);
        }

        [Fact]
        public async Task Remove_DeletesClientAndSendsEvent()
        {
            var repository = new Mock<IClientRepository>();
            var eventProducer = new Mock<IEventProducer>();
            var eventProducerFactory = new Mock<IEventProducerFactory>();
            eventProducerFactory.Setup((obj) => obj.Create("client_removed")).Returns(eventProducer.Object);
            var clientId = new ClientId(Guid.NewGuid());
            var service = new ClientService(repository.Object, eventProducerFactory.Object);

            await service.Remove(clientId);

            repository.Verify(obj => obj.Remove(clientId), Times.Once);
            repository.Verify(obj => obj.Save(), Times.Once);
            eventProducer.Verify(obj => obj.Produce(It.Is<IEvent>(ev => ev.Type == nameof(UserRemoved) && ((UserRemoved)ev).ClientId == clientId)), Times.Once);
        }
    }
}

using Moq;
using MyBank.Domain.Commands;
using MyBank.Domain.Repositories;
using MyBank.Domain.Services;
using Xunit;

namespace MyBank.Domain.Tests.Services
{
    public class ClientServiceTests
    {
        [Fact]
        public void Register_CreatesAndPersistsANewClient()
        {
            var repository = new Mock<IClientRepository>();
            var command = new BecomeClient
            {
                Name = "Renan"
            };
            var service = new ClientService(repository.Object);

            var client = service.Register(command);

            Assert.Equal(command.Name, client.Name);
            repository.Verify(obj => obj.Add(It.Is<Client>(r => r.Name == command.Name)), Times.Once);
            repository.Verify(obj => obj.Save(), Times.Once);
        }
    }
}

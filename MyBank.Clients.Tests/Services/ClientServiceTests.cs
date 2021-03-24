using Moq;
using MyBank.Clients.Domain.Commands;
using MyBank.Clients.Domain.Repositories;
using MyBank.Clients.Domain.Services;
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
            var command = new BecomeClient
            {
                Name = "Renan"
            };
            var service = new ClientService(repository.Object);

            var client = await service.Register(command);

            Assert.Equal(command.Name, client.Name.Name);
            repository.Verify(obj => obj.Add(It.Is<Client>(r => r.Name.Name == command.Name)), Times.Once);
            repository.Verify(obj => obj.Save(), Times.Once);
        }
    }
}

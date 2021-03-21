using Moq;
using MyBank.Domain.Repositories;
using MyBank.Domain.Services;
using Xunit;

namespace MyBank.Domain.Tests.Services
{
    public class AccountOpeningServiceTests
    {
        [Fact]
        public void Request_CreatesNewRequestAndPersistToTheDatabase_IfAValidNameIsPassed()
        {
            var clientRepository = new Mock<IClientRepository>();
            var client = new Client("Renan");
            clientRepository.Setup(obj => obj.FindById(client.Id)).Returns(client);
            var service = new AccountOpeningService(clientRepository.Object);

            var request = service.RequestAccountOpening(client.Id);

            Assert.Equal(client.AccountOpeningRequest.Id, request.Id);
            clientRepository.Verify(obj => obj.FindById(client.Id), Times.Once);
            clientRepository.Verify(obj => obj.Save(), Times.Once);
        }
    }
}

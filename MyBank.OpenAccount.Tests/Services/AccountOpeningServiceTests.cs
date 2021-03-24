using Moq;
using MyBank.OpenAccount.Domain.Repositories;
using MyBank.OpenAccount.Domain.Services;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MyBank.OpenAccount.Domain.Tests.Services
{
    public class AccountOpeningServiceTests
    {
        [Fact]
        public async Task Request_CreatesNewRequestAndPersistToTheDatabase_IfAValidNameIsPassed()
        {
            var requestRepository = new Mock<IAccountOpeningRequestRepository>();
            var service = new AccountOpeningService(requestRepository.Object);
            var clientId = Guid.NewGuid();
            var request = await service.RequestAccountOpening(new Commands.RequestAccountOpening { ClientId = clientId });

            Assert.Equal(clientId, request.ClientId.Id);
            requestRepository.Verify(obj => obj.Add(It.Is<AccountOpeningRequest>(r => r.ClientId.Id == clientId)), Times.Once);
            requestRepository.Verify(obj => obj.Save(), Times.Once);
        }
    }
}

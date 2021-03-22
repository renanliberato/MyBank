using Moq;
using MyBank.Domain.Repositories;
using MyBank.Domain.Services;
using System;
using Xunit;

namespace MyBank.Domain.Tests.Services
{
    public class AccountOpeningServiceTests
    {
        [Fact]
        public void Request_CreatesNewRequestAndPersistToTheDatabase_IfAValidNameIsPassed()
        {
            var requestRepository = new Mock<IAccountOpeningRequestRepository>();
            var service = new AccountOpeningService(requestRepository.Object);
            var clientId = Guid.NewGuid();
            var request = service.RequestAccountOpening(clientId);

            Assert.Equal(clientId, request.ClientId);
            requestRepository.Verify(obj => obj.Add(It.Is<AccountOpeningRequest>(r => r.ClientId == clientId)), Times.Once);
            requestRepository.Verify(obj => obj.Save(), Times.Once);
        }
    }
}

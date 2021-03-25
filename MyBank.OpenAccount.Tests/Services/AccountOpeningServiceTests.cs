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

        [Fact]
        public async Task CancelAccountOpening_UpdatesItemAndSavesRepository_IfAValidRequestIdIsSent()
        {
            var request = new AccountOpeningRequest(new MyBank.Domain.Shared.ValueObjects.ClientId(Guid.NewGuid()));
            var requestRepository = new Mock<IAccountOpeningRequestRepository>();
            requestRepository.Setup(obj => obj.FindById(request.Id)).Returns(Task.FromResult(request));
            var service = new AccountOpeningService(requestRepository.Object);
            await service.CancelAccountOpening(request.Id);

            Assert.Equal(AccountOpeningRequestStatus.Cancelled, request.Status);
            requestRepository.Verify(obj => obj.Save(), Times.Once);
        }
    }
}

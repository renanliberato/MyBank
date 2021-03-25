using Moq;
using MyBank.Domain.Shared.Services;
using MyBank.OpenAccount.Domain.Commands;
using MyBank.OpenAccount.Domain.Repositories;
using MyBank.OpenAccount.Domain.Services;
using MyBank.OpenAccount.Domain.ValueObjects;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MyBank.OpenAccount.Domain.Tests.Services
{
    public class AdministrativeAccountOpeningServiceTests
    {
        [Fact]
        public async Task ApproveAccountOpening_UpdatesItemAndSavesRepository_IfAValidRequestIdIsSent()
        {
            var request = new AccountOpeningRequest(new MyBank.Domain.Shared.ValueObjects.ClientId(Guid.NewGuid()));
            var requestRepository = new Mock<IAccountOpeningRequestRepository>();
            var accountClient = new Mock<IAccountClient>();
            requestRepository.Setup(obj => obj.FindById(It.Is<RequestId>(id => id.Id == request.Id.Id))).Returns(Task.FromResult(request));
            var service = new AdministrativeAccountOpeningService(requestRepository.Object, accountClient.Object);

            await service.ApproveAccountOpening(new ApproveAccountOpeningRequest {
                Id = request.Id.Id,
                ClientId = request.ClientId.Id
            });

            Assert.Equal(AccountOpeningRequestStatus.Approved, request.Status);
            requestRepository.Verify(obj => obj.Save(), Times.Once);
            accountClient.Verify(obj => obj.MakeAccount(request.ClientId), Times.Once);
        }

        [Fact]
        public async Task DeclineAccountOpening_UpdatesItemAndSavesRepository_IfAValidRequestIdIsSent()
        {
            var request = new AccountOpeningRequest(new MyBank.Domain.Shared.ValueObjects.ClientId(Guid.NewGuid()));
            var requestRepository = new Mock<IAccountOpeningRequestRepository>();
            var accountClient = new Mock<IAccountClient>();
            requestRepository.Setup(obj => obj.FindById(It.Is<RequestId>(id => id.Id == request.Id.Id))).Returns(Task.FromResult(request));
            var service = new AdministrativeAccountOpeningService(requestRepository.Object, accountClient.Object);

            await service.DeclineAccountOpening(new DeclineAccountOpeningRequest {
                Id = request.Id.Id,
                ClientId = request.ClientId.Id
            });

            Assert.Equal(AccountOpeningRequestStatus.Decline, request.Status);
            requestRepository.Verify(obj => obj.Save(), Times.Once);
        }
    }
}

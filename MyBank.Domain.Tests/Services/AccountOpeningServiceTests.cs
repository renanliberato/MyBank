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
            var repository = new Mock<IAccountOpeningRequestRepository>();
            var name = "Renan";
            var service = new AccountOpeningService(repository.Object);

            var request = service.RequestAccountOpening(name);

            Assert.Equal(name, request.Name);
            repository.Verify(obj => obj.Add(It.Is<AccountOpeningRequest>(r => r.Name == name)), Times.Once);
            repository.Verify(obj => obj.Save(), Times.Once);
        }
    }
}

using Moq;
using MyBank.Domain.Exceptions;
using MyBank.Domain.Repositories;
using MyBank.Domain.Services;
using Xunit;

namespace MyBank.Domain.Tests.Services
{
    public class AccountServiceTests
    {
        [Fact]
        public void Transfer_WithAmountBiggerThanFromAccountBalance_ThrowsException()
        {
            var from = Account.FromExistingData(new AccountNumber(), AccountBalance.FromExistingData(100));
            var to = Account.FromExistingData(new AccountNumber(), AccountBalance.FromExistingData(100));

            var repository = new Mock<IAccountRepository>();
            repository.Setup(obj => obj.FindByNumber(from.Number)).Returns(from);
            repository.Setup(obj => obj.FindByNumber(to.Number)).Returns(to);

            var service = new AccountService(repository.Object);

            Assert.Throws<InsufficientFundsException>(() => service.Transfer(from.Number, to.Number, 200));
        }

        [Fact]
        public void Transfer_WithValidAmount_WithDrawFromOneAccountAndDepositOnAnother()
        {
            var from = Account.FromExistingData(new AccountNumber(), AccountBalance.FromExistingData(100));
            var to = Account.FromExistingData(new AccountNumber(), AccountBalance.FromExistingData(100));

            var repository = new Mock<IAccountRepository>();
            repository.Setup(obj => obj.FindByNumber(from.Number)).Returns(from);
            repository.Setup(obj => obj.FindByNumber(to.Number)).Returns(to);

            var service = new AccountService(repository.Object);

            service.Transfer(from.Number, to.Number, 50);

            Assert.Equal(50, (float)from.Balance);
            Assert.Equal(150, (float)to.Balance);

            repository.Verify(obj => obj.FindByNumber(from.Number), Times.Once);
            repository.Verify(obj => obj.FindByNumber(to.Number), Times.Once);
            repository.Verify(obj => obj.Save(), Times.Once);
        }
    }
}

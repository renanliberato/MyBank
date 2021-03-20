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
        public void Deposit_WithInvalidAmount_ThrowsException()
        {
            var account = Account.FromExistingData(new AccountNumber(), AccountBalance.FromExistingData(100), new Client("Renan"));

            var repository = new Mock<IAccountRepository>();
            repository.Setup(obj => obj.FindByNumber(account.Number)).Returns(account);

            var service = new AccountService(repository.Object);

            Assert.Throws<InvalidDepositAmountException>(() => service.Deposit(account.Number, 0));
            repository.Verify(obj => obj.FindByNumber(account.Number), Times.Once);
        }

        [Fact]
        public void Deposit_WithValidAmount_IncrementsAccountBalanceAndSavesItToTheRepository()
        {
            var account = Account.FromExistingData(new AccountNumber(), AccountBalance.FromExistingData(100), new Client("Renan"));

            var repository = new Mock<IAccountRepository>();
            repository.Setup(obj => obj.FindByNumber(account.Number)).Returns(account);

            var service = new AccountService(repository.Object);

            service.Deposit(account.Number, 50);

            Assert.Equal(150, account.GetBalance());

            repository.Verify(obj => obj.FindByNumber(account.Number), Times.Once);
            repository.Verify(obj => obj.Save(), Times.Once);
        }

        [Fact]
        public void Transfer_WithAmountBiggerThanFromAccountBalance_ThrowsException()
        {
            var from = Account.FromExistingData(new AccountNumber(), AccountBalance.FromExistingData(100), new Client("Renan"));
            var to = Account.FromExistingData(new AccountNumber(), AccountBalance.FromExistingData(100), new Client("Renan"));

            var repository = new Mock<IAccountRepository>();
            repository.Setup(obj => obj.FindByNumber(from.Number)).Returns(from);
            repository.Setup(obj => obj.FindByNumber(to.Number)).Returns(to);

            var service = new AccountService(repository.Object);

            Assert.Throws<InsufficientFundsException>(() => service.Transfer(from.Number, to.Number, 200));
            repository.Verify(obj => obj.FindByNumber(from.Number), Times.Once);
            repository.Verify(obj => obj.FindByNumber(to.Number), Times.Once);
        }

        [Fact]
        public void Transfer_WithValidAmount_WithDrawFromOneAccountAndDepositOnAnother()
        {
            var from = Account.FromExistingData(new AccountNumber(), AccountBalance.FromExistingData(100), new Client("Renan"));
            var to = Account.FromExistingData(new AccountNumber(), AccountBalance.FromExistingData(100), new Client("Renan"));

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

        [Fact]
        public void Withdraw_WithInvalidAmount_ThrowsException()
        {
            var account = Account.FromExistingData(new AccountNumber(), AccountBalance.FromExistingData(100), new Client("Renan"));

            var repository = new Mock<IAccountRepository>();
            repository.Setup(obj => obj.FindByNumber(account.Number)).Returns(account);

            var service = new AccountService(repository.Object);

            Assert.Throws<InvalidWithdrawAmountException>(() => service.Withdraw(account.Number, 0));
            repository.Verify(obj => obj.FindByNumber(account.Number), Times.Once);
        }

        [Fact]
        public void Withdraw_WithAmountBiggerThanBalance_ThrowsException()
        {
            var account = Account.FromExistingData(new AccountNumber(), AccountBalance.FromExistingData(100), new Client("Renan"));

            var repository = new Mock<IAccountRepository>();
            repository.Setup(obj => obj.FindByNumber(account.Number)).Returns(account);

            var service = new AccountService(repository.Object);

            Assert.Throws<InsufficientFundsException>(() => service.Withdraw(account.Number, 150));
            repository.Verify(obj => obj.FindByNumber(account.Number), Times.Once);
        }

        [Fact]
        public void Withdraw_WithValidAmount_DecrementsAccountBalanceAndSavesItToTheRepository()
        {
            var account = Account.FromExistingData(new AccountNumber(), AccountBalance.FromExistingData(100), new Client("Renan"));

            var repository = new Mock<IAccountRepository>();
            repository.Setup(obj => obj.FindByNumber(account.Number)).Returns(account);

            var service = new AccountService(repository.Object);

            service.Withdraw(account.Number, 50);

            Assert.Equal(50, account.GetBalance());

            repository.Verify(obj => obj.FindByNumber(account.Number), Times.Once);
            repository.Verify(obj => obj.Save(), Times.Once);
        }
    }
}

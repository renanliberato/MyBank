using Moq;
using MyBank.Domain.Exceptions;
using MyBank.Domain.Repositories;
using MyBank.Domain.Services;
using MyBank.Domain.ValueObjects;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MyBank.Domain.Tests.Services
{
    public class AccountServiceTests
    {
        [Fact]
        public async Task Deposit_WithInvalidAmount_ThrowsException()
        {
            var account = Account.FromExistingData(new AccountNumber(), AccountBalance.FromExistingData(100), new ClientId(Guid.NewGuid()));

            var repository = new Mock<IAccountRepository>();
            repository.Setup(obj => obj.FindByNumber(account.Number)).Returns(Task.FromResult(account));

            var service = new AccountService(repository.Object);

            await Assert.ThrowsAsync<InvalidDepositAmountException>(() => service.Deposit(account.Number, 0));

            repository.Verify(obj => obj.FindByNumber(account.Number), Times.Once);
        }

        [Fact]
        public async Task Deposit_WithValidAmount_IncrementsAccountBalanceAndSavesItToTheRepository()
        {
            var account = Account.FromExistingData(new AccountNumber(), AccountBalance.FromExistingData(100), new ClientId(Guid.NewGuid()));

            var repository = new Mock<IAccountRepository>();
            repository.Setup(obj => obj.FindByNumber(account.Number)).Returns(Task.FromResult(account));

            var service = new AccountService(repository.Object);

            await service.Deposit(account.Number, 50);

            Assert.Equal(150, account.GetBalance());

            repository.Verify(obj => obj.FindByNumber(account.Number), Times.Once);
            repository.Verify(obj => obj.Save(), Times.Once);
        }

        [Fact]
        public async Task Transfer_WithAmountBiggerThanFromAccountBalance_ThrowsException()
        {
            var from = Account.FromExistingData(new AccountNumber(), AccountBalance.FromExistingData(100), new ClientId(Guid.NewGuid()));
            var to = Account.FromExistingData(new AccountNumber(), AccountBalance.FromExistingData(100), new ClientId(Guid.NewGuid()));

            var repository = new Mock<IAccountRepository>();
            repository.Setup(obj => obj.FindByNumber(from.Number)).Returns(Task.FromResult(from));
            repository.Setup(obj => obj.FindByNumber(to.Number)).Returns(Task.FromResult(to));

            var service = new AccountService(repository.Object);

            await Assert.ThrowsAsync<InsufficientFundsException>(() => service.Transfer(from.Number, to.Number, 200));
            repository.Verify(obj => obj.FindByNumber(from.Number), Times.Once);
            repository.Verify(obj => obj.FindByNumber(to.Number), Times.Once);
        }

        [Fact]
        public async Task Transfer_WithValidAmount_WithDrawFromOneAccountAndDepositOnAnother()
        {
            var from = Account.FromExistingData(new AccountNumber(), AccountBalance.FromExistingData(100), new ClientId(Guid.NewGuid()));
            var to = Account.FromExistingData(new AccountNumber(), AccountBalance.FromExistingData(100), new ClientId(Guid.NewGuid()));

            var repository = new Mock<IAccountRepository>();
            repository.Setup(obj => obj.FindByNumber(from.Number)).Returns(Task.FromResult(from));
            repository.Setup(obj => obj.FindByNumber(to.Number)).Returns(Task.FromResult(to));

            var service = new AccountService(repository.Object);

            await service.Transfer(from.Number, to.Number, 50);

            Assert.Equal(50, (float)from.Balance);
            Assert.Equal(150, (float)to.Balance);

            repository.Verify(obj => obj.FindByNumber(from.Number), Times.Once);
            repository.Verify(obj => obj.FindByNumber(to.Number), Times.Once);
            repository.Verify(obj => obj.Save(), Times.Once);
        }

        [Fact]
        public async Task Withdraw_WithInvalidAmount_ThrowsException()
        {
            var account = Account.FromExistingData(new AccountNumber(), AccountBalance.FromExistingData(100), new ClientId(Guid.NewGuid()));

            var repository = new Mock<IAccountRepository>();
            repository.Setup(obj => obj.FindByNumber(account.Number)).Returns(Task.FromResult(account));

            var service = new AccountService(repository.Object);

            await Assert.ThrowsAsync<InvalidWithdrawAmountException>(() => service.Withdraw(account.Number, 0));
            repository.Verify(obj => obj.FindByNumber(account.Number), Times.Once);
        }

        [Fact]
        public async Task Withdraw_WithAmountBiggerThanBalance_ThrowsException()
        {
            var account = Account.FromExistingData(new AccountNumber(), AccountBalance.FromExistingData(100), new ClientId(Guid.NewGuid()));

            var repository = new Mock<IAccountRepository>();
            repository.Setup(obj => obj.FindByNumber(account.Number)).Returns(Task.FromResult(account));

            var service = new AccountService(repository.Object);

            await Assert.ThrowsAsync<InsufficientFundsException>(() => service.Withdraw(account.Number, 150));
            repository.Verify(obj => obj.FindByNumber(account.Number), Times.Once);
        }

        [Fact]
        public async Task Withdraw_WithValidAmount_DecrementsAccountBalanceAndSavesItToTheRepository()
        {
            var account = Account.FromExistingData(new AccountNumber(), AccountBalance.FromExistingData(100), new ClientId(Guid.NewGuid()));

            var repository = new Mock<IAccountRepository>();
            repository.Setup(obj => obj.FindByNumber(account.Number)).Returns(Task.FromResult(account));

            var service = new AccountService(repository.Object);

            await service.Withdraw(account.Number, 50);

            Assert.Equal(50, account.GetBalance());

            repository.Verify(obj => obj.FindByNumber(account.Number), Times.Once);
            repository.Verify(obj => obj.Save(), Times.Once);
        }
    }
}

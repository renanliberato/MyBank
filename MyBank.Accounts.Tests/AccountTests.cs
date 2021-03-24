using MyBank.Accounts.Domain.Exceptions;
using MyBank.Domain.Shared.ValueObjects;
using System;
using Xunit;
using MyBank.Accounts.Domain;

namespace MyBank.Accounts.Domain.Tests
{
    public class AccountTests
    {
        private readonly Random random = new Random();

        [Fact]
        public void Deposit_WithZeroValue_ThrowsException()
        {
            var account = Account.Create(new ClientId(Guid.NewGuid()));

            Assert.Throws<InvalidDepositAmountException>(() => account.Deposit(0));
        }

        [Fact]
        public void Deposit_WithValidValue_IncreasesAccountBalance()
        {
            var account = Account.Create(new ClientId(Guid.NewGuid()));
            var amountToDeposit = random.Next(1, 100);

            account.Deposit(amountToDeposit);

            Assert.Equal(amountToDeposit, account.GetBalance());
        }

        [Fact]
        public void Withdraw_WithZeroValue_ThrowsException()
        {
            var account = Account.Create(new ClientId(Guid.NewGuid()));

            Assert.Throws<InvalidWithdrawAmountException>(() => account.Withdraw(0));
        }

        [Fact]
        public void Withdraw_WithAValueBiggerThanBalance_ThrowsException()
        {
            var account = Account.Create(new ClientId(Guid.NewGuid()));
            var amountToWithdraw = random.Next(1, 100);

            account.Deposit(amountToWithdraw - 1);

            Assert.Throws<InsufficientFundsException>(() => account.Withdraw(amountToWithdraw));
        }

        [Fact]
        public void Withdraw_WithValidValue_DecreasesAccountBalance()
        {
            var account = Account.Create(new ClientId(Guid.NewGuid()));
            var amountToWithdraw = random.Next(1, 100);

            account.Deposit(amountToWithdraw);

            account.Withdraw(amountToWithdraw);

            Assert.Equal(0, account.GetBalance());
        }
    }
}

using MyBank.Domain.Exceptions;
using System;
using Xunit;

namespace MyBank.Domain.Tests
{
    public class AccountTests
    {
        private readonly Random random = new Random();

        [Fact]
        public void Deposit_WithZeroValue_ThrowsException()
        {
            var account = new Account();

            Assert.Throws<InvalidDepositException>(() => account.Deposit(0));
        }

        [Fact]
        public void Deposit_WithValidValue_IncreasesAccountBalance()
        {
            var account = new Account();
            var amountToDeposit = random.Next(1, 100);

            account.Deposit(amountToDeposit);

            Assert.Equal(amountToDeposit, account.GetBalance());
        }
    }
}

using System;
using System.Linq;
using Xunit;

namespace MyBank.Accounts.Domain.Tests
{
    public class AccountNumberTests
    {
        [Fact]
        public void AccountNumber_IsCreated_WithAnUniqueNumber()
        {
            var qtdOfNumbers = new Random().Next(0, 100);

            var list = Enumerable
                .Range(0, qtdOfNumbers)
                .Select(i => (new AccountNumber()).Number)
                .ToArray();

            var quantityOfDifferentNumbers = list
                .GroupBy(i => i)
                .Count();

            Assert.Equal(list.Length, quantityOfDifferentNumbers);
        }

        [Fact]
        public void AccountNumber_IsCreated_With36Characters()
        {
            var accountNumber = new AccountNumber();

            Assert.Equal(36, accountNumber.Number.Length);
        }

        [Fact]
        public void AccountNumber_ToString_ReturnsItsNumber()
        {
            var accountNumber = new AccountNumber();

            Assert.Equal(accountNumber.Number, accountNumber.ToString());
        }
    }
}

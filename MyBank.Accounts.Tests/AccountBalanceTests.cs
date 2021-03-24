using Xunit;

namespace MyBank.Accounts.Domain.Tests
{
    public class AccountBalanceTests
    {
        [Fact]
        public void AccountBalance_IsCreated_Empty()
        {
            var balance = new AccountBalance();

            Assert.Equal(0, balance.Amount);
        }
    }
}

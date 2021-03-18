using Xunit;

namespace MyBank.Domain.Tests
{
    public class AccountBalanceTests
    {
        [Fact]
        public void AccountBalance_IsCreated_Empty()
        {
            var balance = new AccountBalance();

            Assert.Equal(0, balance.Value);
        }
    }
}

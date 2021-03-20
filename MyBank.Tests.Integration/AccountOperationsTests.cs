using Microsoft.AspNetCore.Mvc.Testing;
using MyBank.Domain.Commands;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace MyBank.Tests.Integration
{
    public class AccountOperationsTests
    : BaseTests
    {
        public AccountOperationsTests(WebApplicationFactory<Startup> factory) : base(factory)
        {
        }

        [Fact]
        public async Task Deposit_IncreasesAccountBalance_WhenSendingValidInformation()
        {
            // Arrange
            var bankAccount = CreateBankAccount();

            var client = _factory.CreateClient();

            // Act
            var command = new MakeDeposit
            {
                AccountNumber = bankAccount.GetNumber(),
                Amount = 100
            };
            var content = JsonContent.Create(command);
            var response = await client.PostAsync("/api/account/deposit", content);

            // Assert
            response.EnsureSuccessStatusCode();

            var updatedBalance = context.Clients.Where(a => a.Account.Id == bankAccount.Id).Select(c => c.Account.Balance.Amount).First();

            Assert.Equal(100, updatedBalance);
        }

        [Fact]
        public async Task Withdraw_DecreasesAccountBalance_WhenSendingValidInformation()
        {
            // Arrange
            var bankAccount = CreateBankAccount(100);

            var client = _factory.CreateClient();

            // Act
            var command = new MakeWithdraw
            {
                AccountNumber = bankAccount.GetNumber(),
                Amount = 50
            };
            var content = JsonContent.Create(command);
            var response = await client.PostAsync("/api/account/withdraw", content);

            // Assert
            response.EnsureSuccessStatusCode();

            var updatedBalance = context.Clients.Where(a => a.Account.Id == bankAccount.Id).Select(c => c.Account.Balance.Amount).First();

            Assert.Equal(50, updatedBalance);
        }

        [Fact]
        public async Task Transfer_DecreasesAndIncreasesAccountsBalances_WhenSendingValidInformation()
        {
            // Arrange
            var from = CreateBankAccount(100);
            var to = CreateBankAccount(100);

            var client = _factory.CreateClient();

            // Act
            var command = new MakeTransfer
            {
                From = from.GetNumber(),
                To = to.GetNumber(),
                Amount = 50
            };
            var content = JsonContent.Create(command);
            var response = await client.PostAsync("/api/account/transfer", content);

            // Assert
            response.EnsureSuccessStatusCode();

            var fromBalance = context.Clients.Where(a => a.Account.Id == from.Id).Select(c => c.Account.Balance.Amount).First();
            var toBalance = context.Clients.Where(a => a.Account.Id == to.Id).Select(c => c.Account.Balance.Amount).First();

            Assert.Equal(50, fromBalance);
            Assert.Equal(150, toBalance);
        }
    }
}

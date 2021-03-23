using Microsoft.AspNetCore.Mvc.Testing;
using MyBank.Accounts.WebAPI;
using MyBank.Domain.Commands;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace MyBank.Accounts.Tests.Integration
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
            var response = await client.PostAsync("/api/deposit", content);

            // Assert
            response.EnsureSuccessStatusCode();

            var updatedAccount = context.Accounts.First(a => a.Id == bankAccount.Id);

            Assert.Equal(100, updatedAccount.Balance.Amount);
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
            var response = await client.PostAsync("/api/withdraw", content);

            // Assert
            response.EnsureSuccessStatusCode();

            var updatedBalance = context.Accounts.Where(a => a.Id == bankAccount.Id).First();

            Assert.Equal(50, updatedBalance.Balance.Amount);
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
            var response = await client.PostAsync("/api/transfer", content);

            // Assert
            response.EnsureSuccessStatusCode();

            var fromBalance = context.Accounts.Where(a => a.Id == from.Id).Select(c => c.Balance.Amount).First();
            var toBalance = context.Accounts.Where(a => a.Id == to.Id).Select(c => c.Balance.Amount).First();

            Assert.Equal(50, fromBalance);
            Assert.Equal(150, toBalance);
        }
    }
}

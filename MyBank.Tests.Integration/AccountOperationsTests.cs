using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using MyBank.Domain;
using MyBank.Infrastructure.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq;
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
        public async Task CreateAccount_ReturnsAccountInformation()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.PostAsync("/api/account", default);

            // Assert
            response.EnsureSuccessStatusCode();

            Assert.Equal("application/json; charset=utf-8",
                response.Content.Headers.ContentType.ToString());

            var account = JsonConvert.DeserializeObject<Account>(await response.Content.ReadAsStringAsync());
            Assert.NotNull(account);
            Assert.NotEmpty(account.Id);
            Assert.NotEmpty(account.GetNumber());
            Assert.Equal(0, account.GetBalance());

            Assert.NotNull(context.Accounts.FirstOrDefault(a => a.Id == account.Id));
        }
    }
}

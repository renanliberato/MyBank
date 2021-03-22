using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using MyBank.Domain;
using MyBank.Domain.Commands;
using Newtonsoft.Json;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace MyBank.Tests.Integration
{
    public class AccountOpeningOperationsTests
    : BaseTests
    {
        public AccountOpeningOperationsTests(WebApplicationFactory<Startup> factory) : base(factory)
        {
        }

        [Fact]
        public async Task CreateRequest_CreateRegisterAndPersistsToDatabase_WhenClientIdExists()
        {
            // Arrange
            var bankClient = CreateBankClient("Renan");

            var client = _factory.CreateClient();

            // Act
            var command = new RequestAccountOpening
            {
                ClientId = bankClient.Id
            };
            var content = JsonContent.Create(command);
            var response = await client.PostAsync("/api/accountopeningrequest", content);

            // Assert
            response.EnsureSuccessStatusCode();
            var requestFromResponse = JsonConvert.DeserializeObject<AccountOpeningRequest>(await response.Content.ReadAsStringAsync());

            var request = context.AccountOpeningRequests.Where(c => c.Id == requestFromResponse.Id).AsNoTracking().FirstOrDefault();

            Assert.NotNull(request);
        }
    }
}

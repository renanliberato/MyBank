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
    public class AdministrativeAccountOpeningOperationsTests
    : BaseTests
    {
        public AdministrativeAccountOpeningOperationsTests(WebApplicationFactory<Startup> factory) : base(factory)
        {
        }

        [Fact]
        public async Task Approve_DoesChangesAndPersistToDatabase()
        {
            // Arrange
            var bankClient = CreateBankClient("Renan");
            var request = CreateAccountOpeningRequest(bankClient);

            var client = _factory.CreateClient();

            // Act
            var command = new ApproveAccountOpeningRequest
            {
                ClientId = bankClient.Id,
                Id = request.Id
            };
            var content = JsonContent.Create(command);
            var response = await client.PostAsync("/api/administrativeaccountopeningrequest/approve", content);

            // Assert
            response.EnsureSuccessStatusCode();
            var requestFromResponse = JsonConvert.DeserializeObject<AccountOpeningRequest>(await response.Content.ReadAsStringAsync());

            var requestStatus = context.AccountOpeningRequests.Where(c => c.Id == requestFromResponse.Id).AsNoTracking().FirstOrDefault().Status;
            var accountFromClient = context.Accounts.FirstOrDefault(c => c.ClientId == bankClient.Id);

            Assert.NotNull(request);
            Assert.Equal(AccountOpeningRequestStatus.Approved, requestStatus);
            Assert.NotNull(accountFromClient);
        }

        [Fact]
        public async Task Decline_DoesChangesAndPersistToDatabase()
        {
            // Arrange
            var bankClient = CreateBankClient("Renan");
            var request = CreateAccountOpeningRequest(bankClient);

            var client = _factory.CreateClient();

            // Act
            var command = new DeclineAccountOpeningRequest
            {
                ClientId = bankClient.Id,
                Id = request.Id
            };
            var content = JsonContent.Create(command);
            var response = await client.PostAsync("/api/administrativeaccountopeningrequest/decline", content);

            // Assert
            response.EnsureSuccessStatusCode();
            var requestFromResponse = JsonConvert.DeserializeObject<AccountOpeningRequest>(await response.Content.ReadAsStringAsync());

            var requestStatus = context.AccountOpeningRequests.Where(c => c.Id == requestFromResponse.Id).AsNoTracking().FirstOrDefault().Status;

            Assert.NotNull(request);
            Assert.Equal(AccountOpeningRequestStatus.Decline, requestStatus);
        }
    }
}

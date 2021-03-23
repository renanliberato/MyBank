using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using MyBank.Domain;
using MyBank.Domain.Commands;
using MyBank.Domain.ValueObjects;
using MyBank.OpenAccount.WebAPI;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace MyBank.OpenAccount.Tests.Integration
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
            var bankClientId = new ClientId(Guid.NewGuid());

            var client = _factory.CreateClient();

            // Act
            var command = new RequestAccountOpening
            {
                ClientId = bankClientId.Id
            };
            var content = JsonContent.Create(command);
            var response = await client.PostAsync("/api/open", content);

            // Assert
            response.EnsureSuccessStatusCode();
            var requestFromResponse = JsonConvert.DeserializeObject<AccountOpeningRequest>(await response.Content.ReadAsStringAsync());

            var request = context.AccountOpeningRequests.Where(c => c.Id == requestFromResponse.Id).AsNoTracking().FirstOrDefault();

            Assert.NotNull(request);
        }
    }
}

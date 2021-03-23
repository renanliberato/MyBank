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
    public class AdministrativeAccountOpeningOperationsTests
    : BaseTests
    {
        public AdministrativeAccountOpeningOperationsTests(WebApplicationFactory<Startup> factory) : base(factory)
        {
        }

        //[Fact]
        //public async Task Approve_DoesChangesAndPersistToDatabase()
        //{
        //    // Arrange
        //    var bankClientId = new ClientId(Guid.NewGuid());
        //    var request = CreateAccountOpeningRequest(bankClientId);

        //    var client = _factory.CreateClient();

        //    // Act
        //    var command = new ApproveAccountOpeningRequest
        //    {
        //        ClientId = bankClientId.Id,
        //        Id = request.Id.Id
        //    };
        //    var content = JsonContent.Create(command);
        //    var response = await client.PostAsync("/api/administrative/approve", content);

        //    // Assert
        //    response.EnsureSuccessStatusCode();
        //    var requestFromResponse = JsonConvert.DeserializeObject<AccountOpeningRequest>(await response.Content.ReadAsStringAsync());

        //    var requestStatus = context.AccountOpeningRequests.Where(c => c.Id == requestFromResponse.Id).AsNoTracking().FirstOrDefault().Status;

        //    Assert.NotNull(request);
        //    Assert.Equal(AccountOpeningRequestStatus.Approved, requestStatus);
        //}

        [Fact]
        public async Task Decline_DoesChangesAndPersistToDatabase()
        {
            // Arrange
            var bankClientId = new ClientId(Guid.NewGuid());
            var request = CreateAccountOpeningRequest(bankClientId);

            var client = _factory.CreateClient();

            // Act
            var command = new DeclineAccountOpeningRequest
            {
                ClientId = bankClientId.Id,
                Id = request.Id.Id
            };
            var content = JsonContent.Create(command);
            var response = await client.PostAsync("/api/administrative/decline", content);

            // Assert
            response.EnsureSuccessStatusCode();
            var requestFromResponse = JsonConvert.DeserializeObject<AccountOpeningRequest>(await response.Content.ReadAsStringAsync());

            var requestStatus = context.AccountOpeningRequests.Where(c => c.Id == requestFromResponse.Id).AsNoTracking().FirstOrDefault().Status;

            Assert.NotNull(request);
            Assert.Equal(AccountOpeningRequestStatus.Decline, requestStatus);
        }
    }
}

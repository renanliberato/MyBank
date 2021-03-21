using Microsoft.AspNetCore.Mvc.Testing;
using MyBank.Domain;
using MyBank.Domain.Commands;
using Newtonsoft.Json;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace MyBank.Tests.Integration
{
    public class ClientOperationsTests
    : BaseTests
    {
        public ClientOperationsTests(WebApplicationFactory<Startup> factory) : base(factory)
        {
        }

        [Fact]
        public async Task Register_CreatesClientAndPersistToDatabase_WhenAValidNameIsSent()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var command = new BecomeClient
            {
                Name = "Renan"
            };
            var content = JsonContent.Create(command);
            var response = await client.PostAsync("/api/client", content);

            // Assert
            response.EnsureSuccessStatusCode();
            var clientFromResponse = JsonConvert.DeserializeObject<Client>(await response.Content.ReadAsStringAsync());

            var clientFromDatabase = context.Clients.FirstOrDefault(c => c.Id == clientFromResponse.Id);

            Assert.NotNull(clientFromDatabase);
        }
    }
}

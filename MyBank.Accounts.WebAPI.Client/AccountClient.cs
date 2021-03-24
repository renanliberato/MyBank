using MyBank.Accounts.Domain;
using MyBank.Accounts.Domain.Commands;
using MyBank.Domain.Shared.Services;
using MyBank.Domain.Shared.ValueObjects;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MyBank.Accounts.WebAPI.Client
{
    public class AccountClient : IAccountClient
    {
        private readonly HttpClient httpClient;

        public AccountClient()
        {
            this.httpClient = new HttpClient();
        }

        public async Task<AccountId> MakeAccount(ClientId clientId)
        {
            var content = JsonContent.Create(new MakeAccount
            {
                ClientId = clientId
            });

            HttpResponseMessage response = await httpClient.PostAsync("https://localhost:5000/api/accounts/create", content);

            response.EnsureSuccessStatusCode();

            return JsonConvert.DeserializeObject<AccountId>(await response.Content.ReadAsStringAsync());
        }
    }
}

using MyBank.Domain;
using MyBank.Domain.Commands;
using MyBank.Domain.Services;
using MyBank.Domain.ValueObjects;
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

        public async Task<Account> MakeAccount(ClientId clientId)
        {
            var content = JsonContent.Create(new MakeAccount
            {
                ClientId = clientId
            });

            HttpResponseMessage response = await httpClient.PostAsync("https://localhost:5000/api/accounts/create", content);

            response.EnsureSuccessStatusCode();

            return JsonConvert.DeserializeObject<Account>(await response.Content.ReadAsStringAsync());
        }
    }
}

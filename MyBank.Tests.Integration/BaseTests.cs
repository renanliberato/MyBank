using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using MyBank.Domain;
using MyBank.Domain.ValueObjects;
using System;
using Xunit;

namespace MyBank.Tests.Integration
{
    [Collection(nameof(SystemTestCollectionDefinition))]
    public class BaseTests : IClassFixture<WebApplicationFactory<Startup>>, IDisposable
    {
        protected readonly WebApplicationFactory<Startup> _factory;
        protected readonly AccountContext context;

        public BaseTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            AccountContext.DbName = "accounts_test.db";

            context = new AccountContext();

            context.Database.EnsureDeleted();
            context.Database.Migrate();
        }


        public void Dispose()
        {
            context.Dispose();
        }

        protected Client CreateBankClient(string name)
        {
            var client = new Client(new ClientName(name));
            context.Clients.Add(client);
            context.SaveChanges();

            return client;
        }

        protected AccountOpeningRequest CreateAccountOpeningRequest(Client client)
        {
            var request = new AccountOpeningRequest(client.Id);
            context.AccountOpeningRequests.Add(request);
            context.SaveChanges();

            return request;
        }

        protected Account CreateBankAccount(float balance = 0)
        {
            var client = CreateBankClient("Renan");
            var account = Account.Create(client.Id);
            context.Accounts.Add(account);

            if (balance > 0)
                account.Deposit(balance);

            context.SaveChanges();

            return account;
        }
    }

    [CollectionDefinition(nameof(SystemTestCollectionDefinition), DisableParallelization = true)]
    public class SystemTestCollectionDefinition { }
}

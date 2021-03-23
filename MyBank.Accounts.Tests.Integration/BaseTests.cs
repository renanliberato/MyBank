using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using MyBank.Accounts.Infrastructure.EntityFrameworkCore;
using MyBank.Domain;
using MyBank.Domain.ValueObjects;
using System;
using Xunit;

namespace MyBank.Accounts.Tests.Integration
{
    [Collection(nameof(SystemTestCollectionDefinition))]
    public class BaseTests : IClassFixture<WebApplicationFactory<MyBank.Accounts.WebAPI.Startup>>, IDisposable
    {
        protected readonly WebApplicationFactory<MyBank.Accounts.WebAPI.Startup> _factory;
        protected readonly AccountContext context;

        public BaseTests(WebApplicationFactory<MyBank.Accounts.WebAPI.Startup> factory)
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

        protected Account CreateBankAccount(float balance = 0)
        {
            var account = Account.Create(new ClientId(Guid.NewGuid()));
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

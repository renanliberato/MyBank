using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using MyBank.Domain;
using MyBank.Domain.Repositories;
using MyBank.Domain.Services;
using MyBank.Infrastructure.EntityFrameworkCore;
using System;
using System.IO;
using Xunit;

namespace MyBank.Tests.Integration
{
    public class BaseTests : IClassFixture<WebApplicationFactory<MyBank.Startup>>, IDisposable
    {
        protected readonly WebApplicationFactory<MyBank.Startup> _factory;
        protected readonly AccountContext context;

        public BaseTests(WebApplicationFactory<MyBank.Startup> factory)
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
            var client = new Client(name);
            context.Clients.Add(client);
            context.SaveChanges();

            return client;
        }
        
        protected Account CreateBankAccount(float balance = 0)
        {
            var client = CreateBankClient("Renan");

            client.CreateAccount();
            client.Account.Deposit(balance);
            context.SaveChanges();

            return client.Account;
        }
    }
}

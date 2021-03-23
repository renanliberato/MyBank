using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using MyBank.Domain;
using MyBank.Domain.Repositories;
using MyBank.Domain.Services;
using MyBank.Domain.ValueObjects;
using MyBank.OpenAccount.Infrastructure.EntityFrameworkCore;
using MyBank.OpenAccount.WebAPI;
using System;
using System.IO;
using Xunit;

namespace MyBank.OpenAccount.Tests.Integration
{
    [Collection(nameof(SystemTestCollectionDefinition))]
    public class BaseTests : IClassFixture<WebApplicationFactory<Startup>>, IDisposable
    {
        protected readonly WebApplicationFactory<Startup> _factory;
        protected readonly OpenAccountContext context;

        public BaseTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            OpenAccountContext.DbName = "openaccount_test.db";

            context = new OpenAccountContext();

            context.Database.EnsureDeleted();
            context.Database.Migrate();
        }


        public void Dispose()
        {
            context.Dispose();
        }

        protected AccountOpeningRequest CreateAccountOpeningRequest(ClientId clientId)
        {
            var request = new AccountOpeningRequest(clientId);
            context.AccountOpeningRequests.Add(request);
            context.SaveChanges();

            return request;
        }
    }

    [CollectionDefinition(nameof(SystemTestCollectionDefinition), DisableParallelization = true)]
    public class SystemTestCollectionDefinition { }
}

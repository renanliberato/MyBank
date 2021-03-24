using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using MyBank.Clients.Infrastructure.EntityFrameworkCore;
using MyBank.Clients.WebAPI;
using System;
using Xunit;

namespace MyBank.Clients.Tests.Integration
{
    [Collection(nameof(SystemTestCollectionDefinition))]
    public class BaseTests : IClassFixture<WebApplicationFactory<Startup>>, IDisposable
    {
        protected readonly WebApplicationFactory<Startup> _factory;
        protected readonly ClientContext context;

        public BaseTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            ClientContext.DbName = "clients_test.db";

            context = new ClientContext();

            context.Database.EnsureDeleted();
            context.Database.Migrate();
        }


        public void Dispose()
        {
            context.Dispose();
        }
    }

    [CollectionDefinition(nameof(SystemTestCollectionDefinition), DisableParallelization = true)]
    public class SystemTestCollectionDefinition { }
}

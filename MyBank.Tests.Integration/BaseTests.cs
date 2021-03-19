using Microsoft.AspNetCore.Mvc.Testing;
using MyBank.Infrastructure.EntityFrameworkCore;
using System;
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
            AccountContext.Path = @"Data Source=C:\Users\renan\source\repos\MyBank\accounts_test.db";

            context = new AccountContext();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}

using Microsoft.Extensions.DependencyInjection;
using MyBank.Domain.Repositories;
using MyBank.Domain.Services;
using MyBank.Infrastructure.EntityFrameworkCore;
using MyBank.Infrastructure.EntityFrameworkCore.Repositories;
using System;

namespace MyBank.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new ServiceCollection()
                .AddDbContext<AccountContext>()
                .AddTransient<IAccountRepository, AccountRepository>()
                .AddTransient<IAccountService, AccountService>()
                .BuildServiceProvider();

            var service = container.GetService<IAccountService>();
            var account = service.CreateAccount();

            Console.WriteLine(account.Number);
            Console.WriteLine(account.Balance);
        }
    }
}

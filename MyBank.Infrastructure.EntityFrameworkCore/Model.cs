using Microsoft.EntityFrameworkCore;
using MyBank.Domain;
using MyBank.Domain.ValueObjects;
using System.IO;

namespace MyBank.Infrastructure.EntityFrameworkCore
{
    public class AccountContext : DbContext
    {
        public static string DbName { get; set; } = "accounts.db";

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<AccountOpeningRequest> AccountOpeningRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Client>().HasKey(obj => obj.Id);
            modelBuilder.Entity<Client>().Property(obj => obj.Id)
                .HasConversion(
                    obj => obj.Id,
                    id => new ClientId(id));
            modelBuilder.Entity<Client>().Property(obj => obj.Name)
                .HasConversion(
                    obj => obj.Name,
                    name => new ClientName(name));

            modelBuilder.Entity<Account>().HasKey(obj => obj.Id);
            modelBuilder.Entity<Account>().Property(obj => obj.Id)
                .HasConversion(
                    obj => obj.Id,
                    id => new AccountId(id));
            modelBuilder.Entity<Account>().Property(obj => obj.Number)
                .HasConversion(
                    obj => obj.Number,
                    number => AccountNumber.FromNumber(number));
            modelBuilder.Entity<Account>().Property(obj => obj.Balance)
                .HasConversion(
                    obj => obj.Amount,
                    amount => new AccountBalance(amount));
            modelBuilder.Entity<Account>().Property(obj => obj.ClientId)
                .HasConversion(
                    obj => obj.Id,
                    id => new ClientId(id));

            modelBuilder.Entity<AccountOpeningRequest>().HasKey(obj => obj.Id);
            modelBuilder.Entity<AccountOpeningRequest>().Property(obj => obj.Id)
                .HasConversion(
                    obj => obj.Id,
                    id => new RequestId(id));
            modelBuilder.Entity<AccountOpeningRequest>().Property(obj => obj.ClientId)
                .HasConversion(
                    obj => obj.Id,
                    id => new ClientId(id));
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($@"Data Source = {Directory.GetCurrentDirectory()}{Path.DirectorySeparatorChar}{DbName}");
    }
}

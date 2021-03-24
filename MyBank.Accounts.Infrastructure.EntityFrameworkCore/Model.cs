using Microsoft.EntityFrameworkCore;
using MyBank.Accounts.Domain;
using MyBank.Domain.Shared.ValueObjects;
using System.IO;

namespace MyBank.Accounts.Infrastructure.EntityFrameworkCore
{
    public class AccountContext : DbContext
    {
        public static string DbName { get; set; } = "accounts.db";

        public DbSet<Account> Accounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);

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
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($@"Data Source = {Directory.GetCurrentDirectory()}{Path.DirectorySeparatorChar}{DbName}");
    }
}

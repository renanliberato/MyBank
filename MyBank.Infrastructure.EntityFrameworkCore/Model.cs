using Microsoft.EntityFrameworkCore;
using MyBank.Domain;
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
            modelBuilder.Entity<Client>().OwnsOne(
                a => a.Account,
                a => {
                    a.WithOwner(b => b.Client);
                    a.HasKey(obj => obj.Id);
                    a.OwnsOne(
                        a => a.Number, a =>
                        {
                            a.Property(b => b.Number).HasColumnName("Number");
                        });

                    a.OwnsOne(
                        a => a.Balance, a =>
                        {
                            a.Property(b => b.Amount).HasColumnName("Amount");
                        });
                });

            modelBuilder.Entity<Client>().OwnsOne(
                a => a.AccountOpeningRequest,
                a =>
                {
                    a.HasKey(b => b.Id);
                });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($@"Data Source = {Directory.GetCurrentDirectory()}{Path.DirectorySeparatorChar}{DbName}");
    }
}

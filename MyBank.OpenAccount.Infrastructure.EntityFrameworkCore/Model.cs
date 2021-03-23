using Microsoft.EntityFrameworkCore;
using MyBank.Domain;
using MyBank.Domain.ValueObjects;
using System.IO;

namespace MyBank.OpenAccount.Infrastructure.EntityFrameworkCore
{
    public class OpenAccountContext : DbContext
    {
        public static string DbName { get; set; } = "openaccounts.db";

        public DbSet<AccountOpeningRequest> AccountOpeningRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);

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

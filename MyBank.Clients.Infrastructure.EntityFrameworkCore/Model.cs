using Microsoft.EntityFrameworkCore;
using MyBank.Domain;
using MyBank.Domain.ValueObjects;
using System.IO;

namespace MyBank.Clients.Infrastructure.EntityFrameworkCore
{
    public class ClientContext : DbContext
    {
        public static string DbName { get; set; } = "clients.db";

        public DbSet<Client> Clients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>().HasKey(obj => obj.Id);
            modelBuilder.Entity<Client>().Property(obj => obj.Id)
                .HasConversion(
                    obj => obj.Id,
                    id => new ClientId(id));
            modelBuilder.Entity<Client>().Property(obj => obj.Name)
                .HasConversion(
                    obj => obj.Name,
                    name => new ClientName(name));
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($@"Data Source = {Directory.GetCurrentDirectory()}{Path.DirectorySeparatorChar}{DbName}");
    }
}

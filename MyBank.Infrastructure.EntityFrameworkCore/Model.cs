using Microsoft.EntityFrameworkCore;
using MyBank.Domain;

namespace MyBank.Infrastructure.EntityFrameworkCore
{
    public class AccountContext : DbContext
    {
        //public static string Path { get; set; } = @"Data Source=C:\Users\renan\source\repos\MyBank\accounts.db";
        public static string Path { get; set; } = @"Data Source=C:\Users\renan\source\repos\MyBank\accounts_test.db";

        public DbSet<Account> Accounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Account>().HasKey(obj => obj.Id);
            modelBuilder.Entity<Account>().OwnsOne(
                a => a.Number, a =>
                {
                    a.Property(b => b.Number).HasColumnName("Number");
                });

            modelBuilder.Entity<Account>().OwnsOne(
                a => a.Balance, a =>
                {
                    a.Property(b => b.Amount).HasColumnName("Amount");
                });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite(Path);
    }
}

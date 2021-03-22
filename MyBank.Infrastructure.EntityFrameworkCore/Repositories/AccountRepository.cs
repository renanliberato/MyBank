using MyBank.Domain;
using MyBank.Domain.Repositories;
using System.Linq;

namespace MyBank.Infrastructure.EntityFrameworkCore.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AccountContext context;

        public AccountRepository(AccountContext context)
        {
            this.context = context;
        }

        public void Add(Account account)
        {
            this.context.Accounts.Add(account);
        }

        public Account FindByNumber(AccountNumber number)
        {
            return this.context.Accounts.First(a => a.Number.Number == number.Number);
        }

        public void Save()
        {
            this.context.SaveChanges();
        }
    }
}

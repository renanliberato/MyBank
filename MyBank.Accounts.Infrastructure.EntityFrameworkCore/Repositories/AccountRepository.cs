using Microsoft.EntityFrameworkCore;
using MyBank.Accounts.Domain;
using MyBank.Accounts.Domain.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace MyBank.Accounts.Infrastructure.EntityFrameworkCore.Repositories
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

        public Task<Account> FindByNumber(AccountNumber number)
        {
            return this.context.Accounts.FirstAsync(a => a.Number == number);
        }

        public Task Save()
        {
            return this.context.SaveChangesAsync();
        }
    }
}

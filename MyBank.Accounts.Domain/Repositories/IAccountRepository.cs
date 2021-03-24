using System.Threading.Tasks;

namespace MyBank.Accounts.Domain.Repositories
{
    public interface IAccountRepository
    {
        Task<Account> FindByNumber(AccountNumber number);
        Task Save();
        void Add(Account account);
    }
}

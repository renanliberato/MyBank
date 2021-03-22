using System.Threading.Tasks;

namespace MyBank.Domain.Repositories
{
    public interface IAccountRepository
    {
        Task<Account> FindByNumber(AccountNumber number);
        Task Save();
        void Add(Account account);
    }
}

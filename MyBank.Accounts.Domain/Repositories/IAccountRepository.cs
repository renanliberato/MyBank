using MyBank.Domain.Shared.ValueObjects;
using System.Threading.Tasks;

namespace MyBank.Accounts.Domain.Repositories
{
    public interface IAccountRepository
    {
        Task<Account> FindByNumber(AccountNumber number);
        Task Save();
        Task RemoveFromClientId(ClientId clientId);
        void Add(Account account);
    }
}

using MyBank.Accounts.Domain.Commands;
using MyBank.Domain.Shared.ValueObjects;
using System.Threading.Tasks;

namespace MyBank.Accounts.Domain.Services
{
    public interface IAccountService
    {
        Task Transfer(AccountNumber fromNumber, AccountNumber toNumber, float amount);
        Task Deposit(AccountNumber accountNumber, float amount);
        Task Withdraw(AccountNumber accountNumber, float amount);
        Task RemoveAcountFromRemovedClient(ClientId clientId);
        Task<AccountId> MakeAccount(MakeAccount command);
    }
}
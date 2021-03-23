using MyBank.Domain.Commands;
using System.Threading.Tasks;

namespace MyBank.Domain.Services
{
    public interface IAccountService
    {
        Task Transfer(AccountNumber fromNumber, AccountNumber toNumber, float amount);
        Task Deposit(AccountNumber accountNumber, float amount);
        Task Withdraw(AccountNumber accountNumber, float amount);
        Task<Account> MakeAccount(MakeAccount command);
    }
}
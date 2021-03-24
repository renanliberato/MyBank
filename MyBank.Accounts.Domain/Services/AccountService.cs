using MyBank.Accounts.Domain.Commands;
using MyBank.Accounts.Domain.Repositories;
using MyBank.Domain.Shared.ValueObjects;
using System.Threading.Tasks;

namespace MyBank.Accounts.Domain.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            this.accountRepository = accountRepository;
        }

        public async Task<AccountId> MakeAccount(MakeAccount command)
        {
            var account = Account.Create(command.ClientId);
            accountRepository.Add(account);

            await accountRepository.Save();

            return account.Id;
        }

        public async Task Deposit(AccountNumber accountNumber, float amount)
        {
            var account = await accountRepository.FindByNumber(accountNumber);
            
            account.Deposit(amount);

            await accountRepository.Save();
        }

        public async Task Transfer(AccountNumber fromNumber, AccountNumber toNumber, float amount)
        {
            var from = await accountRepository.FindByNumber(fromNumber);
            var to = await accountRepository.FindByNumber(toNumber);

            from.Withdraw(amount);
            to.Deposit(amount);

            await accountRepository.Save();
        }

        public async Task Withdraw(AccountNumber accountNumber, float amount)
        {
            var account = await accountRepository.FindByNumber(accountNumber);

            account.Withdraw(amount);

            await accountRepository.Save();
        }
    }
}

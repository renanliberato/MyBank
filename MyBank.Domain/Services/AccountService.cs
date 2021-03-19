using MyBank.Domain.Repositories;

namespace MyBank.Domain.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            this.accountRepository = accountRepository;
        }

        public Account CreateAccount()
        {
            var account = new Account();
            
            accountRepository.Add(account);

            accountRepository.Save();

            return account;
        }

        public void Deposit(AccountNumber accountNumber, float amount)
        {
            var account = accountRepository.FindByNumber(accountNumber);
            
            account.Deposit(amount);
        }

        public void Transfer(AccountNumber fromNumber, AccountNumber toNumber, float amount)
        {
            var from = accountRepository.FindByNumber(fromNumber);
            var to = accountRepository.FindByNumber(toNumber);

            from.Withdraw(amount);
            to.Deposit(amount);

            accountRepository.Save();
        }

        public void Withdraw(AccountNumber accountNumber, float amount)
        {
            var account = accountRepository.FindByNumber(accountNumber);

            account.Withdraw(amount);
        }
    }
}

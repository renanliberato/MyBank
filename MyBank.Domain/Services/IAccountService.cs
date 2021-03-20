namespace MyBank.Domain.Services
{
    public interface IAccountService
    {
        Account CreateAccount(Client client);
        void Transfer(AccountNumber fromNumber, AccountNumber toNumber, float amount);
        void Deposit(AccountNumber accountNumber, float amount);
        void Withdraw(AccountNumber accountNumber, float amount);
    }
}
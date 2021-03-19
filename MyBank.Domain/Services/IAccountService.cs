namespace MyBank.Domain.Services
{
    public interface IAccountService
    {
        Account CreateAccount();
        void Transfer(AccountNumber fromNumber, AccountNumber toNumber, float amount);
    }
}
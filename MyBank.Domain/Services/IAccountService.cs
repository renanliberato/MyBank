namespace MyBank.Domain.Services
{
    public interface IAccountService
    {
        void Transfer(AccountNumber fromNumber, AccountNumber toNumber, float amount);
    }
}
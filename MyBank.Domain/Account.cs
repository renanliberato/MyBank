using MyBank.Domain.Exceptions;

namespace MyBank.Domain
{
    public class Account
    {
        public AccountNumber Number { get; private set; }
        public AccountBalance Balance { get; private set; }

        public Account()
        {
            this.Number = new AccountNumber();
            this.Balance = new AccountBalance();
        }

        private Account(AccountNumber number, AccountBalance balance)
        {
            this.Number = number;
            this.Balance = balance;
        }

        public static Account FromExistingData(AccountNumber number, AccountBalance balance)
        {
            return new Account(number, balance);
        }

        public float GetBalance()
        {
            return (float)this.Balance;
        }

        public void Deposit(float amount)
        {
            if (amount <= 0)
                throw new InvalidDepositAmountException("Amount to deposit must be greater than zero");

            this.Balance.Increase(amount);
        }

        public void Withdraw(float amount)
        {
            if (amount <= 0)
                throw new InvalidWithdrawAmountException("Amount to withdraw must be grater than zero");

            if ((float)this.Balance < amount)
                throw new InsufficientFundsException("You do not have balance to this operation");

            this.Balance.Decrease(amount);
        }
    }
}

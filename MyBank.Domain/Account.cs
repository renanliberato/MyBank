using MyBank.Domain.Exceptions;
using System;

namespace MyBank.Domain
{
    public class Account
    {
        public AccountNumber Number { get; set; }
        public AccountBalance Balance { get; set; }

        public Account()
        {
            this.Number = new AccountNumber();
            this.Balance = new AccountBalance();
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
                throw new InsufficientFundsException("You do not have balance to this withdrawal operation");

            this.Balance.Decrease(amount);
        }
    }
}

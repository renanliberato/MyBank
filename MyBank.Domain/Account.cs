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
                throw new InvalidDepositException("Amount to deposit must be greater than zero");

            this.Balance.Increase(amount);
        }
    }
}

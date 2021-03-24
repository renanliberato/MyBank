using MyBank.Accounts.Domain.Exceptions;
using MyBank.Domain.Shared.ValueObjects;
using System.Runtime.Serialization;

namespace MyBank.Accounts.Domain
{
    [DataContract]
    public class Account
    {
        [DataMember]
        public AccountId Id { get; private set; }
        [DataMember]
        public ClientId ClientId { get; private set; }
        [DataMember]
        public AccountNumber Number { get; private set; }
        [DataMember]
        public AccountBalance Balance { get; private set; }

        public Account()
        {
            this.Number = new AccountNumber();
            this.Balance = new AccountBalance();
            
            this.Id = new AccountId(this.Number.Number);
        }

        private Account(AccountNumber number, AccountBalance balance, ClientId clientId)
        {
            this.Number = number;
            this.Balance = balance;
            this.ClientId = clientId;

            this.Id = new AccountId(this.Number.Number);
        }
        
        public static Account Create(ClientId clientId)
        {
            var instance = new Account(new AccountNumber(), new AccountBalance(), clientId);

            instance.Id = new AccountId(instance.Number.Number);

            return instance;
        }

        public static Account FromExistingData(AccountNumber number, AccountBalance balance, ClientId clientId)
        {
            return new Account(number, balance, clientId);
        }

        public float GetBalance()
        {
            return (float)this.Balance;
        }

        public void Deposit(float amount)
        {
            if (amount <= 0)
                throw new InvalidDepositAmountException("Amount to deposit must be greater than zero");

            this.Balance = new AccountBalance(this.Balance.Amount + amount);
        }

        public void Withdraw(float amount)
        {
            if (amount <= 0)
                throw new InvalidWithdrawAmountException("Amount to withdraw must be grater than zero");

            if ((float)this.Balance < amount)
                throw new InsufficientFundsException("You do not have balance to this operation");

            this.Balance = new AccountBalance(this.Balance.Amount - amount);
        }

        public string GetNumber()
        {
            return Number.Number;
        }
    }
}

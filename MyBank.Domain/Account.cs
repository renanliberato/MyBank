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
    }
}

using System;

namespace MyBank.Domain
{
    public class AccountNumber
    {
        public string Number { get; }

        public AccountNumber()
        {
            this.Number = Guid.NewGuid().ToString();
        }

        public override string ToString()
        {
            return Number;
        }
    }
}

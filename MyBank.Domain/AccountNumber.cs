using System;

namespace MyBank.Domain
{
    public class AccountNumber
    {
        public string Value { get; }
        
        public AccountNumber()
        {
            this.Value = Guid.NewGuid().ToString();
        }

        public override string ToString()
        {
            return Value;
        }
    }
}

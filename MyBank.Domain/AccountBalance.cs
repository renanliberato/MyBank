using System;

namespace MyBank.Domain
{
    public class AccountBalance
    {
        public float Value { get; private set; }

        public override string ToString()
        {
            return $"Balance: {this.Value}";
        }
    }
}

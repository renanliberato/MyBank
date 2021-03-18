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

        public void Increase(float amount)
        {
            this.Value += amount;
        }

        public static explicit operator float(AccountBalance balance)
        {
            return balance.Value;
        }

        internal void Decrease(float amount)
        {
            this.Value -= amount;
        }
    }
}

using System;

namespace MyBank.Domain
{
    public class AccountBalance
    {
        public float Value { get; private set; }

        public AccountBalance() { }

        private AccountBalance(float amount)
        {
            this.Value = amount;
        }

        public static AccountBalance FromExistingData(float amount)
        {
            return new AccountBalance(amount);
        }

        public void Increase(float amount)
        {
            this.Value += amount;
        }

        public void Decrease(float amount)
        {
            this.Value -= amount;
        }

        public override string ToString()
        {
            return $"Balance: {this.Value}";
        }

        public static explicit operator float(AccountBalance balance)
        {
            return balance.Value;
        }
    }
}

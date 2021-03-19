using System.Runtime.Serialization;

namespace MyBank.Domain
{
    [DataContract]
    public class AccountBalance
    {
        [DataMember]
        public float Amount { get; private set; }

        public AccountBalance() { }

        private AccountBalance(float amount)
        {
            this.Amount = amount;
        }

        public static AccountBalance FromExistingData(float amount)
        {
            return new AccountBalance(amount);
        }

        public void Increase(float amount)
        {
            this.Amount += amount;
        }

        public void Decrease(float amount)
        {
            this.Amount -= amount;
        }

        public override string ToString()
        {
            return $"Balance: {this.Amount}";
        }

        public static explicit operator float(AccountBalance balance)
        {
            return balance.Amount;
        }
    }
}

using System;
using System.Runtime.Serialization;

namespace MyBank.Domain
{
    [DataContract]
    public class AccountNumber : IEquatable<AccountNumber>
    {
        [DataMember]
        public string Number { get; }

        public AccountNumber()
        {
            this.Number = Guid.NewGuid().ToString();
        }

        private AccountNumber(string number)
        {
            this.Number = Guid.Parse(number).ToString();
        }

        public static AccountNumber FromNumber(string number)
        {
            return new AccountNumber(number);
        }

        public override string ToString()
        {
            return Number;
        }

        public bool Equals(AccountNumber other)
        {
            return this.Number == other.Number;
        }
    }
}

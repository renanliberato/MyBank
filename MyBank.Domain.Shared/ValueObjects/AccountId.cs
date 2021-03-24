using System;

namespace MyBank.Domain.Shared.ValueObjects
{
    public class AccountId : IEquatable<AccountId>
    {
        public AccountId(string id)
        {
            if (id.Length != 36)
                throw new Exception("AccountId must have 36 characters");

            Id = id;
        }

        public string Id { get; private set; }

        public bool Equals(AccountId other)
        {
            return this.Id == other.Id;
        }
    }
}

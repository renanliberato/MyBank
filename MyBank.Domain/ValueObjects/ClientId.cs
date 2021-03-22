using System;

namespace MyBank.Domain.ValueObjects
{
    public class ClientId : IEquatable<ClientId>
    {
        public ClientId(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }

        public bool Equals(ClientId other)
        {
            return this.Id == other.Id;
        }
    }
}

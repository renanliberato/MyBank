using System;

namespace MyBank.Domain.ValueObjects
{
    public class RequestId : IEquatable<RequestId>
    {
        public RequestId(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }

        public bool Equals(RequestId other)
        {
            return this.Id == other.Id;
        }
    }
}

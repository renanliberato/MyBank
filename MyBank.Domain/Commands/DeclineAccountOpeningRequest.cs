using System;
using System.Runtime.Serialization;

namespace MyBank.Domain.Commands
{
    [DataContract]
    public class DeclineAccountOpeningRequest
    {
        [DataMember]
        public Guid Id { get; set; }
        [DataMember]
        public Guid ClientId { get; set; }
    }
}

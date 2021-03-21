using System;
using System.Runtime.Serialization;

namespace MyBank.Domain.Commands
{
    [DataContract]
    public class RequestAccountOpening
    {
        [DataMember]
        public Guid ClientId { get; set; }
    }
}

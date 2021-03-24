using System;
using System.Runtime.Serialization;

namespace MyBank.OpenAccount.Domain.Commands
{
    [DataContract]
    public class RequestAccountOpening
    {
        [DataMember]
        public Guid ClientId { get; set; }
    }
}

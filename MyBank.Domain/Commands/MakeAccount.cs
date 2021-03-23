using MyBank.Domain.ValueObjects;
using System.Runtime.Serialization;

namespace MyBank.Domain.Commands
{
    [DataContract]
    public class MakeAccount
    {
        [DataMember]
        public ClientId ClientId { get; set; }
    }
}

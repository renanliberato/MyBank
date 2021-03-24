using MyBank.Domain.Shared.ValueObjects;
using System.Runtime.Serialization;

namespace MyBank.Accounts.Domain.Commands
{
    [DataContract]
    public class MakeAccount
    {
        [DataMember]
        public ClientId ClientId { get; set; }
    }
}

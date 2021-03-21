using System.Runtime.Serialization;

namespace MyBank.Domain.Commands
{
    [DataContract]
    public class MakeDeposit
    {
        [DataMember]
        public string AccountNumber { get; set; }
        [DataMember]
        public float Amount { get; set; }
    }
}

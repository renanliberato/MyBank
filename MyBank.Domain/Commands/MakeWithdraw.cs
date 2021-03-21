using System.Runtime.Serialization;

namespace MyBank.Domain.Commands
{
    [DataContract]
    public class MakeWithdraw
    {
        [DataMember]
        public string AccountNumber { get; set; }
        [DataMember]
        public float Amount { get; set; }
    }
}

using System.Runtime.Serialization;

namespace MyBank.Accounts.Domain.Commands
{
    [DataContract]
    public class MakeTransfer
    {
        [DataMember]
        public string From { get; set; }
        [DataMember]
        public string To { get; set; }
        [DataMember]
        public float Amount { get; set; }
    }
}

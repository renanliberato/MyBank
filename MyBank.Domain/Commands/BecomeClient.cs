using System.Runtime.Serialization;

namespace MyBank.Domain.Commands
{
    [DataContract]
    public class BecomeClient
    {
        [DataMember]
        public string Name { get; set; }
    }
}

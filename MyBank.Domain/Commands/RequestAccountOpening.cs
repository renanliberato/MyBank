using System.Runtime.Serialization;

namespace MyBank.Domain.Commands
{
    [DataContract]
    public class RequestAccountOpening
    {
        [DataMember]
        public string Name { get; set; }
    }
}

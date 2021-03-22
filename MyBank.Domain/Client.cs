using System;
using System.Runtime.Serialization;

namespace MyBank.Domain
{
    [DataContract]
    public class Client
    {
        [DataMember]
        public Guid Id { get; private set; }
        [DataMember]
        public string Name { get; private set; }

        public Client(string name)
        {
            this.Id = Guid.NewGuid();
            this.Name = name;
        }
    }
}

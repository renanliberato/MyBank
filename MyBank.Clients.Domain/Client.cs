using MyBank.Clients.Domain.ValueObjects;
using MyBank.Domain.Shared.ValueObjects;
using System;
using System.Runtime.Serialization;

namespace MyBank.Clients.Domain
{
    [DataContract]
    public class Client
    {
        [DataMember]
        public ClientId Id { get; private set; }
        [DataMember]
        public ClientName Name { get; private set; }

        public Client() { }

        public Client(ClientName name)
        {
            this.Id = new ClientId(Guid.NewGuid());
            this.Name = name;
        }
    }
}

using MyBank.Domain.Shared.ValueObjects;
using MyBank.OpenAccount.Domain.ValueObjects;
using System;
using System.Runtime.Serialization;

namespace MyBank.OpenAccount.Domain
{
    [DataContract]
    public class AccountOpeningRequest
    {
        [DataMember]
        public RequestId Id { get; private set; }
        [DataMember]
        public ClientId ClientId { get; private set; }
        [DataMember]
        public AccountOpeningRequestStatus Status { get; private set; }
        [DataMember]
        public DateTime RequestedAt { get; private set; }

        public AccountOpeningRequest() { }

        public AccountOpeningRequest(ClientId clientId)
        {
            this.Id = new RequestId(Guid.NewGuid());
            this.ClientId = clientId;
            this.Status = AccountOpeningRequestStatus.Initial;
            this.RequestedAt = DateTime.UtcNow;
        }

        public void Approve()
        {
            this.Status = AccountOpeningRequestStatus.Approved;
        }

        public void Decline()
        {
            this.Status = AccountOpeningRequestStatus.Decline;
        }
    }
}

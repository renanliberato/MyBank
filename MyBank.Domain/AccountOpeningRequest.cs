﻿using System;
using System.Runtime.Serialization;

namespace MyBank.Domain
{
    [DataContract]
    public class AccountOpeningRequest
    {
        [DataMember]
        public Guid Id { get; private set; }
        [DataMember]
        public string Name { get; private set; }
        [DataMember]
        public AccountOpeningRequestStatus Status { get; private set; }
        [DataMember]
        public DateTime RequestedAt { get; private set; }

        public AccountOpeningRequest(string name)
        {
            this.Id = Guid.NewGuid();
            this.Name = name;
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
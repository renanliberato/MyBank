using System;

namespace MyBank.OpenAccount.Domain.Commands
{
    public class ApproveAccountOpeningRequest
    {
        public Guid Id { get; set; }
        public Guid ClientId { get; set; }
    }
}

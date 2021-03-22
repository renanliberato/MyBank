using System;

namespace MyBank.Domain.Commands
{
    public class ApproveAccountOpeningRequest
    {
        public Guid Id { get; set; }
        public Guid ClientId { get; set; }
    }
}

using MyBank.Domain.Shared.ValueObjects;

namespace MyBank.Domain.Shared.Events
{
    public class UserRemoved : IEvent
    {
        public string Type { get; set; } = nameof(UserRemoved);
        public ClientId ClientId { get; set; }
    }
}

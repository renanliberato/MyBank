namespace MyBank.Domain.Shared.Events
{
    public interface IEvent
    {
        string Type { get; set; }
    }
}

using MyBank.Domain.Commands;

namespace MyBank.Domain.Services
{
    public interface IClientService
    {
        Client Register(BecomeClient command);
    }
}
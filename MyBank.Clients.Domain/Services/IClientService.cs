using MyBank.Clients.Domain.Commands;
using MyBank.Domain.Shared.ValueObjects;
using System.Threading.Tasks;

namespace MyBank.Clients.Domain.Services
{
    public interface IClientService
    {
        Task<Client> Register(BecomeClient command);
        Task Remove(ClientId id);
    }
}
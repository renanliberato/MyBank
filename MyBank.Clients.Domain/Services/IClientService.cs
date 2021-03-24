using MyBank.Clients.Domain.Commands;
using System.Threading.Tasks;

namespace MyBank.Clients.Domain.Services
{
    public interface IClientService
    {
        Task<Client> Register(BecomeClient command);
    }
}
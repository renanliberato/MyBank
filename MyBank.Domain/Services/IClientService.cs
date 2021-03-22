using MyBank.Domain.Commands;
using System.Threading.Tasks;

namespace MyBank.Domain.Services
{
    public interface IClientService
    {
        Task<Client> Register(BecomeClient command);
    }
}
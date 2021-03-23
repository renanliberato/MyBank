using MyBank.Domain.ValueObjects;
using System.Threading.Tasks;

namespace MyBank.Domain.Services
{
    public interface IAccountClient
    {
        Task<Account> MakeAccount(ClientId client);
    }
}

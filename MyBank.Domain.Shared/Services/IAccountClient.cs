using MyBank.Domain.Shared.ValueObjects;
using System.Threading.Tasks;

namespace MyBank.Domain.Shared.Services
{
    public interface IAccountClient
    {
        Task<AccountId> MakeAccount(ClientId client);
    }
}

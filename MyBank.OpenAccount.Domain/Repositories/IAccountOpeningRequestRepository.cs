using MyBank.OpenAccount.Domain.ValueObjects;
using System;
using System.Threading.Tasks;

namespace MyBank.OpenAccount.Domain.Repositories
{
    public interface IAccountOpeningRequestRepository
    {
        Task<AccountOpeningRequest> FindById(RequestId requestId);
        Task Save();
        void Add(AccountOpeningRequest request);
        Task Remove(RequestId id);
    }
}

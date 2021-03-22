using MyBank.Domain.ValueObjects;
using System;
using System.Threading.Tasks;

namespace MyBank.Domain.Repositories
{
    public interface IAccountOpeningRequestRepository
    {
        Task<AccountOpeningRequest> FindById(RequestId requestId);
        Task Save();
        void Add(AccountOpeningRequest request);
    }
}

using System;

namespace MyBank.Domain.Repositories
{
    public interface IAccountOpeningRequestRepository
    {
        AccountOpeningRequest FindById(Guid requestId);
        void Save();
        void Add(AccountOpeningRequest request);
    }
}

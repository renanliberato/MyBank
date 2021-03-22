using Microsoft.EntityFrameworkCore;
using MyBank.Domain;
using MyBank.Domain.Repositories;
using MyBank.Domain.ValueObjects;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MyBank.Infrastructure.EntityFrameworkCore.Repositories
{
    public class AccountOpeningRequestRepository : IAccountOpeningRequestRepository
    {
        private readonly AccountContext context;

        public AccountOpeningRequestRepository(AccountContext context)
        {
            this.context = context;
        }

        public void Add(AccountOpeningRequest request)
        {
            this.context.AccountOpeningRequests.Add(request);
        }

        public Task<AccountOpeningRequest> FindById(RequestId requestId)
        {
            return this.context.AccountOpeningRequests.FirstAsync(a => a.Id == requestId);
        }

        public Task Save()
        {
            return this.context.SaveChangesAsync();
        }
    }
}

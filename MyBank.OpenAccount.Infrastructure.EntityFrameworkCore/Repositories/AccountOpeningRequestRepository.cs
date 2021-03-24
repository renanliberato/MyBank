using Microsoft.EntityFrameworkCore;
using MyBank.OpenAccount.Domain;
using MyBank.OpenAccount.Domain.Repositories;
using MyBank.OpenAccount.Domain.ValueObjects;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MyBank.OpenAccount.Infrastructure.EntityFrameworkCore.Repositories
{
    public class AccountOpeningRequestRepository : IAccountOpeningRequestRepository
    {
        private readonly OpenAccountContext context;

        public AccountOpeningRequestRepository(OpenAccountContext context)
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

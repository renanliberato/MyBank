using MyBank.Domain;
using MyBank.Domain.Repositories;
using System;
using System.Linq;

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

        public AccountOpeningRequest FindById(Guid id)
        {
            return this.context.AccountOpeningRequests.First(a => a.Id == id);
        }

        public void Save()
        {
            this.context.SaveChanges();
        }
    }
}

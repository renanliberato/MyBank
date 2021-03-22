using MyBank.Domain.Commands;
using MyBank.Domain.Repositories;
using MyBank.Domain.ValueObjects;
using System;
using System.Threading.Tasks;

namespace MyBank.Domain.Services
{
    public class AccountOpeningService : IAccountOpeningService
    {
        private readonly IAccountOpeningRequestRepository accountOpeningRequestRepository;

        public AccountOpeningService(IAccountOpeningRequestRepository accountOpeningRequestRepository)
        {
            this.accountOpeningRequestRepository = accountOpeningRequestRepository;
        }

        public async Task<AccountOpeningRequest> RequestAccountOpening(RequestAccountOpening command)
        {
            var request = new AccountOpeningRequest(new ClientId(command.ClientId));
            accountOpeningRequestRepository.Add(request);
            await accountOpeningRequestRepository.Save();

            return request;
        }
    }
}

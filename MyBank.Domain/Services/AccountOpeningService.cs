using MyBank.Domain.Repositories;
using System;

namespace MyBank.Domain.Services
{
    public class AccountOpeningService : IAccountOpeningService
    {
        private readonly IAccountOpeningRequestRepository accountOpeningRequestRepository;

        public AccountOpeningService(IAccountOpeningRequestRepository accountOpeningRequestRepository)
        {
            this.accountOpeningRequestRepository = accountOpeningRequestRepository;
        }

        public AccountOpeningRequest RequestAccountOpening(Guid clientId)
        {
            var request = new AccountOpeningRequest(clientId);
            accountOpeningRequestRepository.Add(request);
            accountOpeningRequestRepository.Save();

            return request;
        }
    }
}

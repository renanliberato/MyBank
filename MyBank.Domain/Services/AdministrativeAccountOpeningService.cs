using MyBank.Domain.Repositories;
using System;

namespace MyBank.Domain.Services
{
    public class AdministrativeAccountOpeningService : IAdministrativeAccountOpeningService
    {
        private readonly IAccountOpeningRequestRepository accountOpeningRequestRepository;
        private readonly IAccountRepository accountRepository;

        public AdministrativeAccountOpeningService(IAccountOpeningRequestRepository accountOpeningRequestRepository, IAccountRepository accountRepository)
        {
            this.accountOpeningRequestRepository = accountOpeningRequestRepository;
            this.accountRepository = accountRepository;
        }

        public AccountOpeningRequest ApproveAccountOpening(Guid clientId, Guid requestId)
        {
            var request = accountOpeningRequestRepository.FindById(requestId);

            if (request.ClientId != clientId)
                throw new Exception("Request is not from this client");

            request.Approve();

            var account = Account.Create(clientId);
            accountRepository.Add(account);
            
            accountRepository.Save();
            accountOpeningRequestRepository.Save();

            return request;
        }

        public AccountOpeningRequest DeclineAccountOpening(Guid requestId)
        {
            var request = accountOpeningRequestRepository.FindById(requestId);

            request.Decline();

            accountOpeningRequestRepository.Save();

            return request;
        }
    }
}

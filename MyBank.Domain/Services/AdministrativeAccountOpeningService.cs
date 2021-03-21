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

        public AccountOpeningRequest ApproveAccountOpening(Guid requestId)
        {
            var request = accountOpeningRequestRepository.FindById(requestId);
            
            request.Approve();
            
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

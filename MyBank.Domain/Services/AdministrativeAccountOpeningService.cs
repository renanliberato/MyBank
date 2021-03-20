using MyBank.Domain.Repositories;
using System;

namespace MyBank.Domain.Services
{
    public class AdministrativeAccountOpeningService : IAdministrativeAccountOpeningService
    {
        private readonly IAccountOpeningRequestRepository accountOpeningRequestRepository;

        public AdministrativeAccountOpeningService(IAccountOpeningRequestRepository accountOpeningRequestRepository)
        {
            this.accountOpeningRequestRepository = accountOpeningRequestRepository;
        }

        public AccountOpeningRequest ApproveAccountOpening(Guid requestId)
        {
            var request = accountOpeningRequestRepository.FindById(requestId);
            
            request.Approve();
            
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

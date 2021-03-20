using MyBank.Domain.Repositories;

namespace MyBank.Domain.Services
{
    public class AccountOpeningService : IAccountOpeningService
    {
        private readonly IAccountOpeningRequestRepository accountOpeningRequestRepository;

        public AccountOpeningService(IAccountOpeningRequestRepository accountOpeningRequestRepository)
        {
            this.accountOpeningRequestRepository = accountOpeningRequestRepository;
        }

        public AccountOpeningRequest RequestAccountOpening(string name)
        {
            var request = new AccountOpeningRequest(name);
            
            accountOpeningRequestRepository.Add(request);
            accountOpeningRequestRepository.Save();

            return request;
        }
    }
}

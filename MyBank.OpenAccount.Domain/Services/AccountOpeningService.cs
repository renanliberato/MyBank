using MyBank.OpenAccount.Domain.Commands;
using MyBank.OpenAccount.Domain.Repositories;
using MyBank.Domain.Shared.ValueObjects;
using System.Threading.Tasks;
using MyBank.OpenAccount.Domain.ValueObjects;

namespace MyBank.OpenAccount.Domain.Services
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

        public async Task CancelAccountOpening(RequestId id) {
            var request = await accountOpeningRequestRepository.FindById(id);
            
            request.Cancel();

            await accountOpeningRequestRepository.Save();
        }
    }
}

using MyBank.OpenAccount.Domain.Commands;
using MyBank.OpenAccount.Domain.Repositories;
using MyBank.Domain.Shared.ValueObjects;
using System.Threading.Tasks;

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
    }
}

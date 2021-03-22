using MyBank.Domain.Commands;
using MyBank.Domain.Repositories;
using MyBank.Domain.ValueObjects;
using System;
using System.Threading.Tasks;

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

        public async Task<AccountOpeningRequest> ApproveAccountOpening(ApproveAccountOpeningRequest command)
        {
            var request = await accountOpeningRequestRepository.FindById(new RequestId(command.Id));

            if (request.ClientId.Id != command.ClientId)
                throw new Exception("Request is not from this client");

            request.Approve();

            var account = Account.Create(request.ClientId);
            accountRepository.Add(account);
            
            await accountRepository.Save();
            await accountOpeningRequestRepository.Save();

            return request;
        }

        public async Task<AccountOpeningRequest> DeclineAccountOpening(DeclineAccountOpeningRequest command)
        {
            var request = await accountOpeningRequestRepository.FindById(new RequestId(command.Id));

            request.Decline();

            await accountOpeningRequestRepository.Save();

            return request;
        }
    }
}

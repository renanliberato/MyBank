using MyBank.Domain.Shared.Services;
using MyBank.OpenAccount.Domain.Commands;
using MyBank.OpenAccount.Domain.Repositories;
using MyBank.OpenAccount.Domain.ValueObjects;
using System;
using System.Threading.Tasks;

namespace MyBank.OpenAccount.Domain.Services
{

    public class AdministrativeAccountOpeningService : IAdministrativeAccountOpeningService
    {
        private readonly IAccountOpeningRequestRepository accountOpeningRequestRepository;
        private readonly IAccountClient accountClient;

        public AdministrativeAccountOpeningService(IAccountOpeningRequestRepository accountOpeningRequestRepository, IAccountClient accountClient)
        {
            this.accountOpeningRequestRepository = accountOpeningRequestRepository;
            this.accountClient = accountClient;
        }

        public async Task<AccountOpeningRequest> ApproveAccountOpening(ApproveAccountOpeningRequest command)
        {
            var request = await accountOpeningRequestRepository.FindById(new RequestId(command.Id));

            if (request.ClientId.Id != command.ClientId)
                throw new Exception("Request is not from this client");

            request.Approve();
            
            await accountOpeningRequestRepository.Save();

            await accountClient.MakeAccount(request.ClientId);


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

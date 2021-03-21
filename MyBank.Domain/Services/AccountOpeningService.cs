using MyBank.Domain.Repositories;
using System;

namespace MyBank.Domain.Services
{
    public class AccountOpeningService : IAccountOpeningService
    {
        private readonly IClientRepository clientRepository;

        public AccountOpeningService(IClientRepository clientRepository)
        {
            this.clientRepository = clientRepository;
        }

        public AccountOpeningRequest RequestAccountOpening(Guid clientId)
        {
            var client = clientRepository.FindById(clientId);
            client.RequestAccountCreation();

            clientRepository.Save();

            return client.AccountOpeningRequest;
        }
    }
}

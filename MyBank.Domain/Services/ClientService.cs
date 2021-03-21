using MyBank.Domain.Commands;
using MyBank.Domain.Repositories;

namespace MyBank.Domain.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository clientRepository;

        public ClientService(IClientRepository clientRepository)
        {
            this.clientRepository = clientRepository;
        }

        public Client Register(BecomeClient command)
        {
            var client = new Client(command.Name);

            clientRepository.Add(client);

            clientRepository.Save();

            return client;
        }
    }
}

using MyBank.Clients.Domain.Commands;
using MyBank.Clients.Domain.Repositories;
using MyBank.Clients.Domain.ValueObjects;
using System.Threading.Tasks;

namespace MyBank.Clients.Domain.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository clientRepository;

        public ClientService(IClientRepository clientRepository)
        {
            this.clientRepository = clientRepository;
        }

        public async Task<Client> Register(BecomeClient command)
        {
            var client = new Client(new ClientName(command.Name));

            clientRepository.Add(client);

            await clientRepository.Save();

            return client;
        }
    }
}

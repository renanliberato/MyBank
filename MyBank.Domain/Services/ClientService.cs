using MyBank.Domain.Commands;
using MyBank.Domain.Repositories;
using MyBank.Domain.ValueObjects;
using System.Threading.Tasks;

namespace MyBank.Domain.Services
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

using MyBank.Clients.Domain.Commands;
using MyBank.Clients.Domain.Repositories;
using MyBank.Clients.Domain.ValueObjects;
using MyBank.Domain.Shared.Events;
using MyBank.Domain.Shared.ValueObjects;
using System.Threading.Tasks;

namespace MyBank.Clients.Domain.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository clientRepository;
        private readonly IEventProducer eventProducer;

        public ClientService(IClientRepository clientRepository, IEventProducerFactory eventProducerFactory)
        {
            this.clientRepository = clientRepository;
            this.eventProducer = eventProducerFactory.Create("clients_removed");
        }

        public async Task<Client> Register(BecomeClient command)
        {
            var client = new Client(new ClientName(command.Name));

            clientRepository.Add(client);

            await clientRepository.Save();

            return client;
        }

        public async Task Remove(ClientId id)
        {
            await clientRepository.Remove(id);

            await clientRepository.Save();

            await eventProducer.Produce(new UserRemoved
            {
                ClientId = id
            });
        }
    }
}

using MyBank.Clients.Infrastructure.EntityFrameworkCore;
using MyBank.Clients.Domain;
using MyBank.Clients.Domain.Repositories;
using System.Threading.Tasks;
using MyBank.Domain.Shared.ValueObjects;

namespace MyBank.Clients.Infrastructure.Infrastructure.EntityFrameworkCore.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly ClientContext context;

        public ClientRepository(ClientContext context)
        {
            this.context = context;
        }

        public void Add(Client client)
        {
            this.context.Clients.Add(client);
        }

        public Task Save()
        {
            return this.context.SaveChangesAsync();
        }
        
        public async Task Remove(ClientId id)
        {
            this.context.Clients.Remove(await this.context.Clients.FindAsync(id));
        }
    }
}

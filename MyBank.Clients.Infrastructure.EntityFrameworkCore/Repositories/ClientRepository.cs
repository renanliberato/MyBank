using MyBank.Clients.Infrastructure.EntityFrameworkCore;
using MyBank.Domain;
using MyBank.Domain.Repositories;
using System.Threading.Tasks;

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
    }
}

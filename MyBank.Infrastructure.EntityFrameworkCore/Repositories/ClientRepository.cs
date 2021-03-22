using Microsoft.EntityFrameworkCore;
using MyBank.Domain;
using MyBank.Domain.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MyBank.Infrastructure.EntityFrameworkCore.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly AccountContext context;

        public ClientRepository(AccountContext context)
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

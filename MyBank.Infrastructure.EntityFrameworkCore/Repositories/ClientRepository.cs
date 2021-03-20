using MyBank.Domain;
using MyBank.Domain.Repositories;
using System;
using System.Linq;

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

        public Client FindById(Guid id)
        {
            return this.context.Clients.First(a => a.Id == id);
        }

        public void Save()
        {
            this.context.SaveChanges();
        }
    }
}

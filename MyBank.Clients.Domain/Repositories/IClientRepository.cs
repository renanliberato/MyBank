using MyBank.Domain.Shared.ValueObjects;
using System;
using System.Threading.Tasks;

namespace MyBank.Clients.Domain.Repositories
{
    public interface IClientRepository
    {
        Task Save();
        void Add(Client client);
        Task Remove(ClientId jd);
    }
}

using System;
using System.Threading.Tasks;

namespace MyBank.Domain.Repositories
{
    public interface IClientRepository
    {
        Task Save();
        void Add(Client client);
    }
}

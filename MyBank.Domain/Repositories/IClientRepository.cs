using System;

namespace MyBank.Domain.Repositories
{
    public interface IClientRepository
    {
        Client FindById(Guid id);
        void Save();
        void Add(Client client);
    }
}

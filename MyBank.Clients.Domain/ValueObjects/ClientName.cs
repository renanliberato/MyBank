using System;

namespace MyBank.Clients.Domain.ValueObjects
{
    public class ClientName
    {
        public ClientName(string name)
        {
            if (name.Length > 100 || name.Length == 0)
                throw new Exception("Name should be between 1 and 100 characters");

            Name = name;
        }

        public string Name { get; private set; }
    }
}

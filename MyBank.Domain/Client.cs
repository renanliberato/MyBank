using System;

namespace MyBank.Domain
{
    public class Client
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public Account Account { get; private set; }
        public AccountOpeningRequest AccountOpeningRequest { get; private set; }

        public Client(string name)
        {
            this.Id = Guid.NewGuid();
            this.Name = name;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace MyBank.Domain.Repositories
{
    public interface IAccountRepository
    {
        Account FindByNumber(AccountNumber number);
    }
}

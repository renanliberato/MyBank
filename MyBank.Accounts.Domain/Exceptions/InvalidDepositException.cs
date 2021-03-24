using System;

namespace MyBank.Accounts.Domain.Exceptions
{
    public class InvalidDepositAmountException : ArgumentException
    {
        public InvalidDepositAmountException(string message) : base(message)
        {
        }
    }
}

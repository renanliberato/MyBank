using System;

namespace MyBank.Domain.Exceptions
{
    public class InvalidDepositAmountException : ArgumentException
    {
        public InvalidDepositAmountException(string message) : base(message)
        {
        }
    }
}

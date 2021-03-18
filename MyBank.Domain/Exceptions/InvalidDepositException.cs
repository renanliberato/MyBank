using System;

namespace MyBank.Domain.Exceptions
{
    public class InvalidDepositException : Exception
    {
        public InvalidDepositException(string message) : base(message)
        {
        }
    }
}

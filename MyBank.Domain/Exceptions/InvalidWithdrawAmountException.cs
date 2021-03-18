using System;

namespace MyBank.Domain.Exceptions
{
    public class InvalidWithdrawAmountException : ArgumentException
    {
        public InvalidWithdrawAmountException(string message) : base(message)
        {
        }
    }
}

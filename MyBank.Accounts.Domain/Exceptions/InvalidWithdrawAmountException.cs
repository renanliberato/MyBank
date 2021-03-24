using System;

namespace MyBank.Accounts.Domain.Exceptions
{
    public class InvalidWithdrawAmountException : ArgumentException
    {
        public InvalidWithdrawAmountException(string message) : base(message)
        {
        }
    }
}

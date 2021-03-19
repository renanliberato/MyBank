using System;
using System.Collections.Generic;
using System.Text;

namespace MyBank.Domain.Commands
{
    public class MakeDeposit
    {
        public string AccountNumber { get; set; }
        public float Amount { get; set; }
    }
}

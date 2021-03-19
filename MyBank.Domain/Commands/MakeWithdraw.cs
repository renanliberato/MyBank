namespace MyBank.Domain.Commands
{
    public class MakeWithdraw
    {
        public string AccountNumber { get; set; }
        public float Amount { get; set; }
    }
}

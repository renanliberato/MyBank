using Microsoft.AspNetCore.Mvc;
using MyBank.Domain;
using MyBank.Domain.Commands;
using MyBank.Domain.Services;

namespace MyBank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService accountService;

        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        [HttpPost]
        [Route("deposit")]
        public void Deposit([FromBody] MakeDeposit command)
        {
            accountService.Deposit(AccountNumber.FromNumber(command.AccountNumber), command.Amount);
        }

        [HttpPost]
        [Route("withdraw")]
        public void Withdraw([FromBody] MakeWithdraw command)
        {
            accountService.Withdraw(AccountNumber.FromNumber(command.AccountNumber), command.Amount);
        }

        [HttpPost]
        [Route("transfer")]
        public void Transfer([FromBody] MakeTransfer command)
        {
            accountService.Transfer(
                AccountNumber.FromNumber(command.From),
                AccountNumber.FromNumber(command.To),
                command.Amount);
        }
    }
}
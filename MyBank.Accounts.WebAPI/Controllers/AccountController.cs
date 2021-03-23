using Microsoft.AspNetCore.Mvc;
using MyBank.Domain;
using MyBank.Domain.Commands;
using MyBank.Domain.Services;
using System.Threading.Tasks;

namespace MyBank.Accounts.WebAPI.Controllers
{
    [Route("api")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService accountService;

        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        [HttpPost]
        [Route("create")]
        public Task<Account> Create([FromBody] MakeAccount command)
        {
            return accountService.MakeAccount(command);
        }
        
        [HttpPost]
        [Route("deposit")]
        public Task Deposit([FromBody] MakeDeposit command)
        {
            return accountService.Deposit(AccountNumber.FromNumber(command.AccountNumber), command.Amount);
        }

        [HttpPost]
        [Route("withdraw")]
        public Task Withdraw([FromBody] MakeWithdraw command)
        {
            return accountService.Withdraw(AccountNumber.FromNumber(command.AccountNumber), command.Amount);
        }

        [HttpPost]
        [Route("transfer")]
        public Task Transfer([FromBody] MakeTransfer command)
        {
            return accountService.Transfer(
                AccountNumber.FromNumber(command.From),
                AccountNumber.FromNumber(command.To),
                command.Amount);
        }
    }
}
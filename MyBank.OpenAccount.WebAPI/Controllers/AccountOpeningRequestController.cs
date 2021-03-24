using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyBank.OpenAccount.Domain;
using MyBank.OpenAccount.Domain.Commands;
using MyBank.OpenAccount.Domain.Services;

namespace MyBank.OpenAccount.WebAPI.Controllers
{
    [Route("api")]
    [ApiController]
    public class AccountOpeningRequestController : ControllerBase
    {
        private IAccountOpeningService accountOpeningService;

        public AccountOpeningRequestController(IAccountOpeningService accountOpeningService)
        {
            this.accountOpeningService = accountOpeningService;
        }

        [HttpPost]
        [Route("open")]
        public Task<AccountOpeningRequest> CreateRequest([FromBody] RequestAccountOpening command)
        {
            return accountOpeningService.RequestAccountOpening(command);
        }
    }
}
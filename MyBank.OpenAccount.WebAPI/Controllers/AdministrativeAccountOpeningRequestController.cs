using Microsoft.AspNetCore.Mvc;
using MyBank.OpenAccount.Domain;
using MyBank.OpenAccount.Domain.Commands;
using MyBank.OpenAccount.Domain.Services;
using System.Threading.Tasks;

namespace MyBank.OpenAccount.WebAPI.Controllers
{
    [Route("api/administrative")]
    [ApiController]
    public class AdministrativeAccountOpeningRequestController : ControllerBase
    {
        private readonly IAdministrativeAccountOpeningService accountOpeningService;

        public AdministrativeAccountOpeningRequestController(IAdministrativeAccountOpeningService accountOpeningService)
        {
            this.accountOpeningService = accountOpeningService;
        }

        [HttpPost]
        [Route("approve")]
        public Task<AccountOpeningRequest> ApproveRequest([FromBody] ApproveAccountOpeningRequest command)
        {
            return accountOpeningService.ApproveAccountOpening(command);
        }

        [HttpPost]
        [Route("decline")]
        public Task<AccountOpeningRequest> DeclineRequest([FromBody] DeclineAccountOpeningRequest command)
        {
            return accountOpeningService.DeclineAccountOpening(command);
        }
    }
}
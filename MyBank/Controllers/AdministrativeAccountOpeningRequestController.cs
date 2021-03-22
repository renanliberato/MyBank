using Microsoft.AspNetCore.Mvc;
using MyBank.Domain;
using MyBank.Domain.Commands;
using MyBank.Domain.Services;
using System.Threading.Tasks;

namespace MyBank.Controllers
{
    [Route("api/[controller]")]
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
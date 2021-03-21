using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBank.Domain;
using MyBank.Domain.Commands;
using MyBank.Domain.Services;

namespace MyBank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountOpeningRequestController : ControllerBase
    {
        private IAccountOpeningService accountOpeningService;

        public AccountOpeningRequestController(IAccountOpeningService accountOpeningService)
        {
            this.accountOpeningService = accountOpeningService;
        }

        [HttpPost]
        public AccountOpeningRequest CreateRequest([FromBody] RequestAccountOpening command)
        {
            return accountOpeningService.RequestAccountOpening(command.ClientId);
        }
    }
}
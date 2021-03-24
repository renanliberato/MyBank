using Microsoft.AspNetCore.Mvc;
using MyBank.Clients.Domain;
using MyBank.Clients.Domain.Commands;
using MyBank.Clients.Domain.Services;
using MyBank.Domain.Shared.ValueObjects;
using System;
using System.Threading.Tasks;

namespace MyBank.Clients.WebAPI.Controllers
{
    [Route("api")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private IClientService clientService;

        public ClientController(IClientService clientService)
        {
            this.clientService = clientService;
        }

        [HttpPost]
        public Task<Client> Register([FromBody] BecomeClient command)
        {
            return clientService.Register(command);
        }

        [HttpDelete]
        public Task Remove()
        {
            return clientService.Remove(new ClientId(Guid.Parse(Request.Query["Id"].ToArray()[0])));
        }
    }
}
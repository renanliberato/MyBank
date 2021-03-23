﻿using Microsoft.AspNetCore.Mvc;
using MyBank.Domain;
using MyBank.Domain.Commands;
using MyBank.Domain.Services;
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
    }
}
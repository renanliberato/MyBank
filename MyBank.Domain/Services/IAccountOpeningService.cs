using MyBank.Domain.Commands;
using System;
using System.Threading.Tasks;

namespace MyBank.Domain.Services
{
    public interface IAccountOpeningService
    {
        Task<AccountOpeningRequest> RequestAccountOpening(RequestAccountOpening command);
    }
}
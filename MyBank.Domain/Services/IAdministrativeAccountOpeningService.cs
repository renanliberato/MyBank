using MyBank.Domain.Commands;
using System;
using System.Threading.Tasks;

namespace MyBank.Domain.Services
{
    public interface IAdministrativeAccountOpeningService
    {
        Task<AccountOpeningRequest> ApproveAccountOpening(ApproveAccountOpeningRequest command);
        Task<AccountOpeningRequest> DeclineAccountOpening(DeclineAccountOpeningRequest command);
    }
}
using MyBank.OpenAccount.Domain.Commands;
using System;
using System.Threading.Tasks;

namespace MyBank.OpenAccount.Domain.Services
{
    public interface IAdministrativeAccountOpeningService
    {
        Task<AccountOpeningRequest> ApproveAccountOpening(ApproveAccountOpeningRequest command);
        Task<AccountOpeningRequest> DeclineAccountOpening(DeclineAccountOpeningRequest command);
    }
}
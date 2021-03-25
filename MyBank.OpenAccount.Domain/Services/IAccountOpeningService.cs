using MyBank.OpenAccount.Domain.Commands;
using MyBank.OpenAccount.Domain.ValueObjects;
using System.Threading.Tasks;

namespace MyBank.OpenAccount.Domain.Services
{
    public interface IAccountOpeningService
    {
        Task<AccountOpeningRequest> RequestAccountOpening(RequestAccountOpening command);
        Task CancelAccountOpening(RequestId id);
    }
}
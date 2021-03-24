using MyBank.OpenAccount.Domain.Commands;
using System.Threading.Tasks;

namespace MyBank.OpenAccount.Domain.Services
{
    public interface IAccountOpeningService
    {
        Task<AccountOpeningRequest> RequestAccountOpening(RequestAccountOpening command);
    }
}
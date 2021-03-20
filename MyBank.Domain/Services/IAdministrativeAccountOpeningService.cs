using System;

namespace MyBank.Domain.Services
{
    public interface IAdministrativeAccountOpeningService
    {
        AccountOpeningRequest ApproveAccountOpening(Guid requestId);
        AccountOpeningRequest DeclineAccountOpening(Guid requestId);
    }
}
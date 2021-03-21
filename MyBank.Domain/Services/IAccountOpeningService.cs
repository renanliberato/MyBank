using System;

namespace MyBank.Domain.Services
{
    public interface IAccountOpeningService
    {
        AccountOpeningRequest RequestAccountOpening(Guid clientId);
    }
}
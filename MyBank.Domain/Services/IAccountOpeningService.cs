namespace MyBank.Domain.Services
{
    public interface IAccountOpeningService
    {
        AccountOpeningRequest RequestAccountOpening(string name);
    }
}
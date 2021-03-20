using Microsoft.AspNetCore.Mvc.Testing;
using MyBank.Domain;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MyBank.Tests.Integration
{
    public class AccountOpeningOperationsTests
    : BaseTests
    {
        public AccountOpeningOperationsTests(WebApplicationFactory<Startup> factory) : base(factory)
        {
        }
    }
}

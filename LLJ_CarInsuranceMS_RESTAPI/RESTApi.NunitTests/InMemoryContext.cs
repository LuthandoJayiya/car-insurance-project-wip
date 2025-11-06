using LLJ_CarInsuranceMS_RESTAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace RESTApi.NunitTests
{
    public static class InMemoryContext
    {
        public static LLJ_CarInsuranceMS_EFDBContext GeneratedData()
        {
            var _contextOptions = new DbContextOptionsBuilder<LLJ_CarInsuranceMS_EFDBContext>()
                .UseInMemoryDatabase("ControllerTest")
                .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;

            return new LLJ_CarInsuranceMS_EFDBContext( _contextOptions );
        }
    }
}

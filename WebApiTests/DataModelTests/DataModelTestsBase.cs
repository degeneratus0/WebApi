using NUnit.Framework;
using System.Net;

namespace WebApiTests.DataModelTests
{
    public abstract class DataModelTestsBase : WebApiTestsBase
    {
        [SetUp]
        public async Task SetupDataModels()
        {
            StringContent testStringContent = TestingUtilities.CreateDefaultStringContent("[]");
            HttpResponseMessage response = await httpClient.PostAsync("/api/datamodel/set", testStringContent);
            TestingUtilities.IsResponseStatus(HttpStatusCode.Created, response.StatusCode);
        }
    }
}

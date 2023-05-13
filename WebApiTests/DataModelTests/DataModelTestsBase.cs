using NUnit.Framework;
using System.Net;
using System.Text;
using System.Text.Json;

namespace WebApiTests.DataModelTests
{
    public abstract class DataModelTestsBase : WebApiTestsBase
    {
        [SetUp]
        public async Task SetupDataModelController()
        {
            StringContent testStringContent = new StringContent(
                JsonSerializer.Serialize(new List<string>()
                    {
                        "item1",
                        "item2",
                        "item3"
                    }),
                Encoding.UTF8,
                "application/json"
                );
            HttpResponseMessage response = await httpClient.PostAsync("/api/datamodel/set", testStringContent);
            TestingUtilities.IsResponseStatus(HttpStatusCode.OK, response.StatusCode);
        }
    }
}

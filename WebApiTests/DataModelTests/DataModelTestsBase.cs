using NUnit.Framework;
using System.Net;
using System.Text;
using System.Text.Json;

namespace WebApiTests.DataModelTests
{
    public abstract class DataModelTestsBase : WebApiTestsBase
    {
        public static readonly List<string> testData = new List<string>()
        {
            "item1",
            "item2",
            "item3"
        };

        [SetUp]
        public async Task SetupDataModels()
        {
            StringContent testStringContent = new StringContent(
                JsonSerializer.Serialize(testData),
                Encoding.UTF8,
                "application/json"
                );
            HttpResponseMessage response = await httpClient.PostAsync("/api/datamodel/set", testStringContent);
            TestingUtilities.IsResponseStatus(HttpStatusCode.Created, response.StatusCode);
        }
    }
}

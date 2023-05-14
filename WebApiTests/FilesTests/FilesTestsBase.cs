using NUnit.Framework;
using System.Net;
using System.Text;
using System.Text.Json;

namespace WebApiTests.FilesTests
{
    [SingleThreaded]
    public abstract class FilesTestsBase : WebApiTestsBase
    {
        public static readonly List<string> testData = new List<string>()
        {
            "text 1",
            "text 2",
            "text 3"
        };

        [SetUp]
        public async Task SetupFiles()
        {
            StringContent testStringContent = new StringContent(
                JsonSerializer.Serialize(testData),
                Encoding.UTF8,
                "application/json"
                );
            HttpResponseMessage response = await httpClient.PostAsync("/api/files/set", testStringContent);
            TestingUtilities.IsResponseStatus(HttpStatusCode.Created, response.StatusCode);
        }

        [OneTimeTearDown]
        public async Task Teardown()
        {
            HttpResponseMessage response = await httpClient.DeleteAsync("/api/files/clear");
            TestingUtilities.IsResponseStatus(HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}
using NUnit.Framework;
using System.Net;

namespace WebApiTests.FilesTests
{
    [SingleThreaded]
    public abstract class FilesTestsBase : WebApiTestsBase
    {
        [SetUp]
        public async Task SetupFiles()
        {
            StringContent testStringContent = TestingUtilities.CreateDefaultStringContent("[]");
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
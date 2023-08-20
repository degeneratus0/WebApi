using NUnit.Framework;
using System.Net;
using WebApi.Models;

namespace WebApiTests.DataModelTests
{
    public class DataModelNegativeTests : DataModelTestsBase
    {
        [Test]
        public async Task GetDataModelByIncorrectId()
        {
            string testId = "_";

            HttpResponseMessage response = await httpClient.GetAsync($"/api/DataModel/{testId}");

            TestingUtilities.IsResponseStatus(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Test]
        public async Task PostEmptyDataModel()
        {
            StringContent emptyStringContent = TestingUtilities.CreateDefaultStringContent("");

            HttpResponseMessage response = await httpClient.PostAsync("/api/DataModel", emptyStringContent);

            TestingUtilities.IsResponseStatus(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Test]
        public async Task PostDataModelEmptyContent()
        {
            StringContent emptyStringContent = TestingUtilities.CreateDefaultStringContentSerializeObject(new DataModel { Id = "_", Content = "" });

            HttpResponseMessage response = await httpClient.PostAsync("/api/DataModel", emptyStringContent);

            TestingUtilities.IsResponseStatus(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Test]
        public async Task PostDataModelOnExistingId()
        {
            DataModel testDataModel = new DataModel { Id = "0", Content = "test" };
            StringContent testStringContent = TestingUtilities.CreateDefaultStringContentSerializeObject(testDataModel);

            HttpResponseMessage response = await httpClient.PostAsync("/api/DataModel", testStringContent);

            TestingUtilities.IsResponseStatus(HttpStatusCode.Conflict, response.StatusCode);
        }

        [Test]
        public async Task PutDataModelByNonExistentId()
        {
            string testContent = "test";
            string testId = "_";
            StringContent testStringContent = TestingUtilities.CreateDefaultStringContentSerializeObject(testContent);

            HttpResponseMessage response = await httpClient.PutAsync($"/api/DataModel/{testId}", testStringContent);

            TestingUtilities.IsResponseStatus(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Test]
        public async Task PutEmptyDataModel()
        {
            string testId = "0";
            StringContent testStringContent = TestingUtilities.CreateDefaultStringContentSerializeObject("");

            HttpResponseMessage response = await httpClient.PutAsync($"/api/DataModel/{testId}", testStringContent);

            TestingUtilities.IsResponseStatus(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Test]
        public async Task DeleteDataModelByIncorrectId()
        {
            string testId = "_";

            HttpResponseMessage response = await httpClient.DeleteAsync($"/api/DataModel/{testId}");

            TestingUtilities.IsResponseStatus(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
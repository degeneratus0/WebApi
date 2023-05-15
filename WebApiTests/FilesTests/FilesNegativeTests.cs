using NUnit.Framework;
using System.Net;
using WebApi.Models;

namespace WebApiTests.FilesTests
{
    public class FilesNegativeTests : FilesTestsBase
    {

        [Test]
        public async Task GetFileByNonExistentId()
        {
            string testId = "nonexistentid";

            HttpResponseMessage response = await httpClient.GetAsync($"/api/files/{testId}");
            
            TestingUtilities.IsResponseStatus(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Test]
        public async Task PostFileWithDataModelNullContent()
        {
            DataModelDTO testDataModel = new DataModelDTO()
            {
                Content = null
            };
            StringContent testStringContent = TestingUtilities.CreateDefaultStringContent(testDataModel);

            HttpResponseMessage response = await httpClient.PostAsync("/api/files", testStringContent);

            TestingUtilities.IsResponseStatus(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Test]
        public async Task PostFileWithIncorrectType()
        {
            StringContent testStringContent = TestingUtilities.CreateDefaultStringContent("incorrecttype");

            HttpResponseMessage response = await httpClient.PostAsync("/api/files", testStringContent);

            TestingUtilities.IsResponseStatus(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Test]
        public async Task PutFileWithDataModelNullContent()
        {
            string testId = "1";
            DataModelDTO testDataModel = new DataModelDTO()
            {
                Content = null
            };
            StringContent testStringContent = TestingUtilities.CreateDefaultStringContent(testDataModel);

            HttpResponseMessage response = await httpClient.PutAsync($"/api/files/{testId}", testStringContent);

            TestingUtilities.IsResponseStatus(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Test]
        public async Task PutFileWithIncorrectType()
        {
            string testId = "1";
            StringContent testStringContent = TestingUtilities.CreateDefaultStringContent("incorrecttype");

            HttpResponseMessage response = await httpClient.PutAsync($"/api/files/{testId}", testStringContent);

            TestingUtilities.IsResponseStatus(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Test]
        public async Task PutFileByNonExistentId()
        {
            string testId = "nonexistentid";
            DataModelDTO testDataModel = new DataModelDTO()
            {
                Content = "test"
            };
            StringContent testStringContent = TestingUtilities.CreateDefaultStringContent(testDataModel);

            HttpResponseMessage response = await httpClient.PutAsync($"/api/files/{testId}", testStringContent);

            TestingUtilities.IsResponseStatus(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Test]
        public async Task DeleteFileByNonExistentId()
        {
            string testId = "nonexistentid";

            HttpResponseMessage response = await httpClient.DeleteAsync($"/api/files/{testId}");

            TestingUtilities.IsResponseStatus(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
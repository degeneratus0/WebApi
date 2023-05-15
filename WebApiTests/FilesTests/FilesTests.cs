using NUnit.Framework;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using WebApi.Models;

namespace WebApiTests.FilesTests
{
    public class FilesTests : FilesTestsBase
    {
        [Test]
        public async Task GetAllFiles()
        {
            List<DataModel> expectedDataModels = new List<DataModel>()
            {
                new DataModel() { Id = "1", Content = "text 1"},
                new DataModel() { Id = "2", Content = "text 2"},
                new DataModel() { Id = "3", Content = "text 3"}
            };

            HttpResponseMessage response = await httpClient.GetAsync("/api/files");
            List<DataModel>? responseContent = await response.Content.ReadFromJsonAsync<List<DataModel>>();

            TestingUtilities.IsResponseStatus(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(
                JsonSerializer.Serialize(expectedDataModels),
                JsonSerializer.Serialize(responseContent)
                );
        }

        [TestCase("1", "text 1")]
        [TestCase("2", "text 2")]
        [TestCase("3", "text 3")]
        public async Task GetFileById(string testId, string expectedContent)
        {
            HttpResponseMessage response = await httpClient.GetAsync($"/api/files/{testId}");
            string responseContent = await response.Content.ReadAsStringAsync();

            TestingUtilities.IsResponseStatus(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(expectedContent, responseContent);
        }

        [Test]
        public async Task PostFile()
        {
            string expectedId = "4";
            DataModelDTO testDataModel = new DataModelDTO()
            {
                Content = "text 4"
            };
            StringContent testStringContent = TestingUtilities.CreateDefaultStringContent(testDataModel);

            HttpResponseMessage response = await httpClient.PostAsync("/api/files", testStringContent);
            string responseContent = await response.Content.ReadAsStringAsync();

            TestingUtilities.IsResponseStatus(HttpStatusCode.Created, response.StatusCode);
            Assert.AreEqual(JsonSerializer.Serialize(testDataModel).ToLower(), responseContent);

            response = await httpClient.GetAsync($"/api/files/{expectedId}");
            responseContent = await response.Content.ReadAsStringAsync();

            TestingUtilities.IsResponseStatus(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(testDataModel.Content, responseContent);
        }

        [Test]
        public async Task PostFileWithEmptyContent()
        {
            string expectedId = "4";
            DataModelDTO testDataModel = new DataModelDTO()
            {
                Content = string.Empty
            };
            StringContent testStringContent = TestingUtilities.CreateDefaultStringContent(testDataModel);

            HttpResponseMessage response = await httpClient.PostAsync("/api/files", testStringContent);
            string responseContent = await response.Content.ReadAsStringAsync();

            TestingUtilities.IsResponseStatus(HttpStatusCode.Created, response.StatusCode);
            Assert.AreEqual(JsonSerializer.Serialize(testDataModel).ToLower(), responseContent);

            response = await httpClient.GetAsync($"/api/files/{expectedId}");
            responseContent = await response.Content.ReadAsStringAsync();

            TestingUtilities.IsResponseStatus(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(testDataModel.Content, responseContent);
        }

        [Test]
        public async Task PutFile()
        {
            string testContent = "test";
            string testId = "1";
            DataModelDTO testDataModel = new DataModelDTO()
            {
                Content = testContent
            };
            StringContent testStringContent = TestingUtilities.CreateDefaultStringContent(testDataModel);

            HttpResponseMessage response = await httpClient.PutAsync($"/api/files/{testId}", testStringContent);

            TestingUtilities.IsResponseStatus(HttpStatusCode.NoContent, response.StatusCode);
            await GetFileById(testId, testContent);
        }

        [Test]
        public async Task DeleteFile()
        {
            string testId = "1";

            HttpResponseMessage response = await httpClient.DeleteAsync($"/api/files/{testId}");

            TestingUtilities.IsResponseStatus(HttpStatusCode.NoContent, response.StatusCode);

            response = await httpClient.GetAsync($"/api/files/{testId}");

            TestingUtilities.IsResponseStatus(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Test]
        public async Task DeleteThenPostFile()
        {
            string idToDelete = "1";

            HttpResponseMessage response = await httpClient.DeleteAsync($"/api/files/{idToDelete}");

            TestingUtilities.IsResponseStatus(HttpStatusCode.NoContent, response.StatusCode);

            response = await httpClient.GetAsync($"/api/files/{idToDelete}");

            TestingUtilities.IsResponseStatus(HttpStatusCode.NotFound, response.StatusCode);

            string expectedId = "4";
            DataModelDTO testDataModel = new DataModelDTO()
            {
                Content = "text 4"
            };
            StringContent testStringContent = TestingUtilities.CreateDefaultStringContent(testDataModel);

            response = await httpClient.PostAsync("/api/files", testStringContent);
            string responseContent = await response.Content.ReadAsStringAsync();

            TestingUtilities.IsResponseStatus(HttpStatusCode.Created, response.StatusCode);
            Assert.AreEqual(JsonSerializer.Serialize(testDataModel).ToLower(), responseContent);

            response = await httpClient.GetAsync($"/api/files/{expectedId}");
            responseContent = await response.Content.ReadAsStringAsync();

            TestingUtilities.IsResponseStatus(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(testDataModel.Content, responseContent);
        }
    }
}
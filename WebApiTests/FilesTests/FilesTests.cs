using NUnit.Framework;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using WebApi.Models;
using WebApi.Models.DTOs;
using WebApi.Models.TestData;

namespace WebApiTests.FilesTests
{
    public class FilesTests : FilesTestsBase
    {
        [Test]
        public async Task GetAllFiles()
        {
            List<DataModel> expectedDataModels = new List<DataModel>();
            for (int i = 0; i < DataModelTestData.TestData.Count; i++)
            {
                expectedDataModels.Add(new DataModel() { Id = i.ToString(), Content = DataModelTestData.TestData[i] });
            }

            HttpResponseMessage response = await httpClient.GetAsync("/api/files");
            List<DataModel>? responseContent = await response.Content.ReadFromJsonAsync<List<DataModel>>();

            TestingUtilities.IsResponseStatus(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(
                JsonSerializer.Serialize(expectedDataModels),
                JsonSerializer.Serialize(responseContent)
                );
        }

        [TestCaseSource(typeof(DataModelTestData), nameof(DataModelTestData.TestDataModels))]
        public async Task GetFileById(DataModel testDataModel)
        {   
            HttpResponseMessage response = await httpClient.GetAsync($"/api/files/{testDataModel.Id}");
            string responseContent = await response.Content.ReadAsStringAsync();

            TestingUtilities.IsResponseStatus(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(testDataModel.Content, responseContent);
        }

        [Test]
        public async Task PostFile()
        {
            string expectedId = DataModelTestData.TestData.Count.ToString();
            DataModelDTO testDataModel = new DataModelDTO()
            {
                Content = "text"
            };
            StringContent testStringContent = TestingUtilities.CreateDefaultStringContentSerializeObject(testDataModel);

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
            string expectedId = DataModelTestData.TestData.Count.ToString();
            DataModelDTO testDataModel = new DataModelDTO()
            {
                Content = string.Empty
            };
            StringContent testStringContent = TestingUtilities.CreateDefaultStringContentSerializeObject(testDataModel);

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
            StringContent testStringContent = TestingUtilities.CreateDefaultStringContentSerializeObject(testDataModel);

            HttpResponseMessage response = await httpClient.PutAsync($"/api/files/{testId}", testStringContent);

            TestingUtilities.IsResponseStatus(HttpStatusCode.NoContent, response.StatusCode);
            await GetFileById(new DataModel() { Id = testId, Content = testContent });
        }

        [Test]
        public async Task DeleteFile()
        {
            string testId = "0";

            HttpResponseMessage response = await httpClient.DeleteAsync($"/api/files/{testId}");

            TestingUtilities.IsResponseStatus(HttpStatusCode.NoContent, response.StatusCode);

            response = await httpClient.GetAsync($"/api/files/{testId}");

            TestingUtilities.IsResponseStatus(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Test]
        public async Task DeleteThenPostFile()
        {
            string idToDelete = "0";

            HttpResponseMessage response = await httpClient.DeleteAsync($"/api/files/{idToDelete}");

            TestingUtilities.IsResponseStatus(HttpStatusCode.NoContent, response.StatusCode);

            response = await httpClient.GetAsync($"/api/files/{idToDelete}");

            TestingUtilities.IsResponseStatus(HttpStatusCode.NotFound, response.StatusCode);

            string expectedId = DataModelTestData.TestData.Count.ToString();
            DataModelDTO testDataModel = new DataModelDTO()
            {
                Content = "text"
            };
            StringContent testStringContent = TestingUtilities.CreateDefaultStringContentSerializeObject(testDataModel);

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
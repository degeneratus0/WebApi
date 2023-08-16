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
            for (int i = 0; i < DataModelTestData.TestDataModels.Count; i++)
            {
                expectedDataModels.Add(new DataModel() { Id = i.ToString(), Content = DataModelTestData.TestDataModels[i].Content });
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
            DataModelDTO expectedDataModel = new DataModelDTO(testDataModel);

            TestingUtilities.IsResponseStatus(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(JsonSerializer.Serialize(expectedDataModel).ToLower(), responseContent);
        }

        [Test]
        public async Task PostFile()
        {
            string expectedId = DataModelTestData.TestDataModels.Count.ToString();
            DataModelDTO testDataModel = new DataModelDTO()
            {
                Content = "text"
            };
            string testDataModelJson = JsonSerializer.Serialize(testDataModel).ToLower();
            StringContent testStringContent = TestingUtilities.CreateDefaultStringContent(testDataModelJson);

            HttpResponseMessage response = await httpClient.PostAsync("/api/files", testStringContent);
            string responseContent = await response.Content.ReadAsStringAsync();

            TestingUtilities.IsResponseStatus(HttpStatusCode.Created, response.StatusCode);
            Assert.AreEqual(testDataModelJson, responseContent);

            response = await httpClient.GetAsync($"/api/files/{expectedId}");
            responseContent = await response.Content.ReadAsStringAsync();

            TestingUtilities.IsResponseStatus(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(testDataModelJson, responseContent);
        }

        [Test]
        public async Task PostFileWithEmptyContent()
        {
            string expectedId = DataModelTestData.TestDataModels.Count.ToString();
            DataModelDTO testDataModel = new DataModelDTO()
            {
                Content = String.Empty
            };
            string testDataModelJson = JsonSerializer.Serialize(testDataModel).ToLower();
            StringContent testStringContent = TestingUtilities.CreateDefaultStringContent(testDataModelJson);

            HttpResponseMessage response = await httpClient.PostAsync("/api/files", testStringContent);
            string responseContent = await response.Content.ReadAsStringAsync();

            TestingUtilities.IsResponseStatus(HttpStatusCode.Created, response.StatusCode);
            Assert.AreEqual(testDataModelJson, responseContent);

            response = await httpClient.GetAsync($"/api/files/{expectedId}");
            responseContent = await response.Content.ReadAsStringAsync();

            TestingUtilities.IsResponseStatus(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(testDataModelJson, responseContent);
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
            //<- Deletion
            string idToDelete = "0";

            HttpResponseMessage response = await httpClient.DeleteAsync($"/api/files/{idToDelete}");

            TestingUtilities.IsResponseStatus(HttpStatusCode.NoContent, response.StatusCode);

            response = await httpClient.GetAsync($"/api/files/{idToDelete}");

            TestingUtilities.IsResponseStatus(HttpStatusCode.NotFound, response.StatusCode);
            //->

            //<- Creation
            string expectedId = DataModelTestData.TestDataModels.Count.ToString();
            DataModelDTO testDataModel = new DataModelDTO()
            {
                Content = "text"
            };
            string testDataModelJson = JsonSerializer.Serialize(testDataModel).ToLower();
            StringContent testStringContent = TestingUtilities.CreateDefaultStringContent(testDataModelJson);

            response = await httpClient.PostAsync("/api/files", testStringContent);
            string responseContent = await response.Content.ReadAsStringAsync();

            TestingUtilities.IsResponseStatus(HttpStatusCode.Created, response.StatusCode);
            Assert.AreEqual(testDataModelJson, responseContent);

            response = await httpClient.GetAsync($"/api/files/{expectedId}");
            responseContent = await response.Content.ReadAsStringAsync();

            TestingUtilities.IsResponseStatus(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(testDataModelJson, responseContent);
            //->
        }
    }
}
using NUnit.Framework;
using System.Net;
using System.Text.Json;
using WebApi.Models;
using WebApi.Models.DTOs;
using WebApi.Models.TestData;

namespace WebApiTests.FilesTests
{
    public class FilesTests : FilesTestsBase
    {
        [Test]
        public async Task SetFiles()
        {
            List<DataModel> expectedDataModels = new List<DataModel>
            {
                new DataModel { Id = "0", Content = "test1"},
                new DataModel { Id = "1", Content = "test2"},
                new DataModel { Id = "2", Content = "test3"}
            };
            string expectedDataModelsJson = JsonSerializer.Serialize(expectedDataModels).ToLower();
            StringContent expectedDataModelsDtoStringContent = TestingUtilities.CreateDefaultStringContentSerializeObject(
                expectedDataModels.Select(x => new DataModelDTO { Content = x.Content })
                );

            HttpResponseMessage response = await httpClient.PostAsync("/api/files/set", expectedDataModelsDtoStringContent);

            TestingUtilities.IsResponseStatus(HttpStatusCode.Created, response.StatusCode);

            response = await httpClient.GetAsync("/api/files");
            TestingUtilities.IsResponseStatus(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(
                expectedDataModelsJson,
                await response.Content.ReadAsStringAsync()
                );
        }

        [Test]
        public async Task GetAllFiles()
        {
            List<DataModel> expectedDataModels = new List<DataModel>(DataModelTestData.TestDataModels);

            HttpResponseMessage response = await httpClient.GetAsync("/api/files");

            TestingUtilities.IsResponseStatus(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(
                JsonSerializer.Serialize(expectedDataModels).ToLower(),
                await response.Content.ReadAsStringAsync()
                );
        }

        [TestCaseSource(typeof(DataModelTestData), nameof(DataModelTestData.TestDataModels))]
        public async Task GetFileById(DataModel testDataModel)
        {
            DataModelDTO expectedDataModel = new DataModelDTO(testDataModel);

            HttpResponseMessage response = await httpClient.GetAsync($"/api/files/{testDataModel.Id}");
            string responseContent = await response.Content.ReadAsStringAsync();

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
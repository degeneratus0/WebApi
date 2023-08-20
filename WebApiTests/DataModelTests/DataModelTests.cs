using NUnit.Framework;
using System.Net;
using System.Text.Json;
using WebApi.Models;
using WebApi.Models.TestData;

namespace WebApiTests.DataModelTests
{
    public class DataModelTests : DataModelTestsBase
    {
        [Test]
        public async Task SetDataModelsDistinct()
        {
            List<DataModel> testDataModels = new List<DataModel>
            {
                new DataModel { Id = "1", Content = "test1"},
                new DataModel { Id = "2", Content = "test2"},
                new DataModel { Id = "3", Content = "test3"}
            };
            string testDataModelsJson = JsonSerializer.Serialize(testDataModels).ToLower();
            StringContent testDataModelsStringContent = TestingUtilities.CreateDefaultStringContent(testDataModelsJson);

            HttpResponseMessage response = await httpClient.PostAsync("/api/DataModel/set", testDataModelsStringContent);

            TestingUtilities.IsResponseStatus(HttpStatusCode.Created, response.StatusCode);

            response = await httpClient.GetAsync($"/api/DataModel");

            TestingUtilities.IsResponseStatus(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(testDataModelsJson, await response.Content.ReadAsStringAsync());
        }

        [Test]
        public async Task SetDataModelsDuplicates()
        {
            List<DataModel> testDataModels = new List<DataModel>
            {
                new DataModel { Id = "1", Content = "test1"},
                new DataModel { Id = "1", Content = "test2"},
                new DataModel { Id = "1", Content = "test3"}
            };
            string testDataModelsDistinctJson = JsonSerializer.Serialize(testDataModels.DistinctBy(x => x.Id).ToList()).ToLower();
            StringContent testDataModelsStringContent = TestingUtilities.CreateDefaultStringContentSerializeObject(testDataModels);

            HttpResponseMessage response = await httpClient.PostAsync("/api/DataModel/set", testDataModelsStringContent);

            TestingUtilities.IsResponseStatus(HttpStatusCode.Created, response.StatusCode);

            response = await httpClient.GetAsync($"/api/DataModel");

            TestingUtilities.IsResponseStatus(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(testDataModelsDistinctJson, await response.Content.ReadAsStringAsync());
        }

        [Test]
        public async Task GetAllDataModels()
        {
            string expectedContent = JsonSerializer.Serialize(DataModelTestData.TestDataModels).ToLower();

            HttpResponseMessage response = await httpClient.GetAsync("/api/DataModel");
            string responseContent = await response.Content.ReadAsStringAsync();

            TestingUtilities.IsResponseStatus(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(expectedContent, responseContent);
        }

        [TestCaseSource(typeof(DataModelTestData), nameof(DataModelTestData.TestDataModels))]
        public async Task GetDataModelById(DataModel testDataModel)
        {
            HttpResponseMessage response = await httpClient.GetAsync($"/api/DataModel/{testDataModel.Id}");
            string responseContent = await response.Content.ReadAsStringAsync();

            TestingUtilities.IsResponseStatus(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(testDataModel.Content, responseContent);
        }

        [Test]
        public async Task PostDataModel()
        {
            DataModel testDataModel = new DataModel { Id = "_", Content = "test" };
            string expectedDataModelJson = JsonSerializer.Serialize(testDataModel).ToLower();
            StringContent testStringContent = TestingUtilities.CreateDefaultStringContent(expectedDataModelJson);

            HttpResponseMessage response = await httpClient.PostAsync("/api/DataModel", testStringContent);
            string responseContent = await response.Content.ReadAsStringAsync();

            TestingUtilities.IsResponseStatus(HttpStatusCode.Created, response.StatusCode);
            Assert.AreEqual(expectedDataModelJson, responseContent);

            response = await httpClient.GetAsync($"/api/DataModel/{testDataModel.Id}");
            responseContent = await response.Content.ReadAsStringAsync();

            TestingUtilities.IsResponseStatus(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(testDataModel.Content, responseContent);
        }

        [Test]
        public async Task PutDataModel()
        {
            string testContent = "test";
            string testId = "0";
            StringContent testStringContent = TestingUtilities.CreateDefaultStringContentSerializeObject(testContent);

            HttpResponseMessage response = await httpClient.PutAsync($"/api/DataModel/{testId}", testStringContent);

            TestingUtilities.IsResponseStatus(HttpStatusCode.NoContent, response.StatusCode);
            await GetDataModelById(new DataModel() { Id = testId, Content = testContent });
        }

        [Test]
        public async Task DeleteDataModel()
        {
            string testId = "0";

            HttpResponseMessage response = await httpClient.DeleteAsync($"/api/DataModel/{testId}");

            TestingUtilities.IsResponseStatus(HttpStatusCode.NoContent, response.StatusCode);

            response = await httpClient.GetAsync($"/api/DataModel/{testId}");

            TestingUtilities.IsResponseStatus(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
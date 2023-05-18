using NUnit.Framework;
using System.Net;
using System.Net.Http.Json;
using WebApi.Models;
using WebApi.Models.TestData;

namespace WebApiTests.DataModelTests
{
    public class DataModelTests : DataModelTestsBase
    {
        [Test]
        public async Task GetAllDataModels()
        {
            List<string> expectedModelsContent = DataModelTestData.TestData;

            HttpResponseMessage response = await httpClient.GetAsync("/api/DataModel");
            List<string>? responseContent = await response.Content.ReadFromJsonAsync<List<string>>();

            TestingUtilities.IsResponseStatus(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(expectedModelsContent, responseContent);
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
            string testContent = "test";
            StringContent testStringContent = TestingUtilities.CreateDefaultStringContentSerializeObject(testContent);

            HttpResponseMessage response = await httpClient.PostAsync("/api/DataModel", testStringContent);
            string responseContent = await response.Content.ReadAsStringAsync();

            TestingUtilities.IsResponseStatus(HttpStatusCode.Created, response.StatusCode);
            Assert.AreEqual(testContent, responseContent);

            response = await httpClient.GetAsync($"/api/DataModel/{DataModelTestData.TestData.Count}");
            responseContent = await response.Content.ReadAsStringAsync();

            TestingUtilities.IsResponseStatus(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(testContent, responseContent);
        }

        [Test]
        public async Task PutDataModel()
        {
            string testContent = "test";
            int testId = 0;
            StringContent testStringContent = TestingUtilities.CreateDefaultStringContentSerializeObject(testContent);

            HttpResponseMessage response = await httpClient.PutAsync($"/api/DataModel/{testId}", testStringContent);

            TestingUtilities.IsResponseStatus(HttpStatusCode.NoContent, response.StatusCode);

            await GetDataModelById(new DataModel() { Id = testId.ToString(), Content = testContent });
        }

        [Test]
        public async Task DeleteDataModel()
        {
            int testId = 0;

            HttpResponseMessage response = await httpClient.GetAsync($"/api/DataModel/{testId}");
            string itemToDelete = await response.Content.ReadAsStringAsync();
            response = await httpClient.DeleteAsync($"/api/DataModel/{testId}");

            TestingUtilities.IsResponseStatus(HttpStatusCode.NoContent, response.StatusCode);

            response = await httpClient.GetAsync($"/api/DataModel/{testId}");
            string actualItem = await response.Content.ReadAsStringAsync();

            Assert.AreNotEqual(itemToDelete, actualItem);
        }
    }
}
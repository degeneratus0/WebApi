using System.Collections.Generic;

namespace WebApi.Models.TestData
{
    internal static class DataModelTestData
    {
        public static readonly List<string> TestData = new List<string>()
        {
            "item 0",
            "item 1",
            "item 2"
        };

        public static readonly List<DataModel> TestDataModels = new List<DataModel>()
        {
            new DataModel() { Id = "0", Content = TestData[0]},
            new DataModel() { Id = "1", Content = TestData[1]},
            new DataModel() { Id = "2", Content = TestData[2]}
        };
    }
}

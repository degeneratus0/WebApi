using System.Collections.Generic;

namespace WebApi.Models.TestData
{
    internal static class DataModelTestData
    {
        public static readonly List<DataModel> TestDataModels = new List<DataModel>()
        {
            new DataModel() { Id = "0", Content = "item 0" },
            new DataModel() { Id = "1", Content = "item 1" },
            new DataModel() { Id = "2", Content = "item 2" }
        };
    }
}

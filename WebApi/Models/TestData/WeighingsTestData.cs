using System.Collections.Generic;

namespace WebApi.Models.TestData
{
    internal static class WeighingsTestData
    {
        public static readonly List<Weighing> TestWeighings = new List<Weighing>
        {
            new Weighing { Id = 1, Item = "Juice", Weight = 200, IdMeasure = 1, TareType = "Box" },
            new Weighing { Id = 2, Item = "Lemonade", Weight = 1, IdMeasure = 2, TareType = "Bottle" }
        };

        public static readonly List<Measure> TestMeasures = new List<Measure>
        {
            new Measure { Id = 1, MeasureName = "g" },
            new Measure { Id = 2, MeasureName = "kg" }
        };
    }
}

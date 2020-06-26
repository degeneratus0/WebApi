using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class Measure
    {
        public int IDMeasure { get; set; }
        public string MeasureName { get; set; }
        public List<Weighing> Weighings { get; set; }
    }
}

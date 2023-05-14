﻿using System.Collections.Generic;

namespace WebApi.Models
{
    public class Measure
    {
        public int IDMeasure { get; set; }
        public string MeasureName { get; set; }
        public List<Weighing> Weighings { get; set; }
    }
}
namespace WebApi.Models
{
    public class Weighing
    {
        public int Id { get; set; }
        public string Item { get; set; }
        public int Weight { get; set; }
        public int? IdMeasure { get; set; }
        public Measure Measure { get; set; }
        public string TareType { get; set; }
    }
}

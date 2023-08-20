namespace WebApi.Models
{
    public class Weighing
    {
        public int Id { get; set; }
        public string Item { get; set; }
        public float Weight { get; set; }
        public int MeasureId { get; set; }
        public Measure Measure { get; set; }
        public string Container { get; set; }
    }
}

namespace WebApi.Models
{
    /// <summary>
    /// Model for "Weighing" entity
    /// </summary>
    public class Weighing
    {
        /// <summary>
        /// Weighing identifier
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Weighted item
        /// </summary>
        public string Item { get; set; }

        /// <summary>
        /// Weight of the item
        /// </summary>
        public float Weight { get; set; }

        /// <summary>
        /// Measure identifier
        /// </summary>
        public int MeasureId { get; set; }

        /// <summary>
        /// Measure used to weigh the item
        /// </summary>
        public Measure Measure { get; set; }

        /// <summary>
        /// Item container
        /// </summary>
        public string Container { get; set; }
    }
}

namespace WebApi.Models.DTOs
{
    /// <summary>
    /// DTO for Weighing model
    /// </summary>
    public class WeighingDTO
    {
        /// <summary>
        /// Weighted item
        /// </summary>
        public string Item { get; set; }

        /// <summary>
        /// Weight of the item
        /// </summary>
        public float Weight { get; set; }

        /// <summary>
        /// Name of the measure used to weigh the item
        /// </summary>
        public string MeasureName { get; set; }

        /// <summary>
        /// Item container
        /// </summary>
        public string Container { get; set; }
    }
}

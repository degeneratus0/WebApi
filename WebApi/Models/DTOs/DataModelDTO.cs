namespace WebApi.Models.DTOs
{
    /// <summary>
    /// DTO for exemplary model
    /// </summary>
    public class DataModelDTO
    {
        /// <summary>
        /// Model content
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Empty constructor
        /// </summary>
        public DataModelDTO() { }

        /// <summary>
        /// Constructor for fast conversion to DTO from DataModel
        /// </summary>
        public DataModelDTO(DataModel dataModel)
        {
            Content = dataModel.Content;
        }
    }
}

namespace WebApi.Models.DTOs
{
    public class DataModelDTO
    {
        public string Content { get; set; }

        public DataModelDTO() { }

        public DataModelDTO(DataModel dataModel)
        {
            Content = dataModel.Content;
        }
    }
}

namespace WebApi.Services.Interfaces
{
    /// <summary>
    /// Describes item conversion to or from DTO
    /// </summary>
    /// <typeparam name="T">Item</typeparam>
    /// <typeparam name="DTO">Item DTO model</typeparam>
    public interface IConverter<T, DTO>
    {
        /// <summary>
        /// Converts item to its DTO model
        /// </summary>
        /// <param name="obj">Item to convert to DTO</param>
        /// <returns>DTO model of the item</returns>
        DTO AsDTO(T obj);

        /// <summary>
        /// Converts DTO model to item
        /// </summary>
        /// <param name="dto">DTO to convert to item</param>
        /// <returns>Item</returns>
        T FromDTO(DTO dto);
    }
}
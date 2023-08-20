using System.Collections.Generic;

namespace WebApi.Services.Interfaces
{
    /// <summary>
    /// Defines a contract for file interactions
    /// </summary>
    /// <typeparam name="T">File model</typeparam>
    /// <typeparam name="DTO">DTO model</typeparam>
    public interface IFileRepository<T, DTO>
    {
        /// <summary>
        /// Creates files for every item passed (erases existing data)
        /// </summary>
        /// <param name="datas">List of contents to create files for</param>
        void Set(List<DTO> datas);

        /// <summary>
        /// Erases all files
        /// </summary>
        void Clear();

        /// <summary>
        /// Gets content of the file with specified id
        /// </summary>
        /// <param name="id">Id of the file to look for</param>
        DTO Read(string id);

        /// <summary>
        /// Gets all files
        /// </summary>
        IEnumerable<T> ReadAll();

        /// <summary>
        /// Creates a file with specified content
        /// </summary>
        /// <param name="data">Content of added file</param>
        void Add(DTO data);

        /// <summary>
        /// Updates the file with specified id
        /// </summary>
        /// <param name="id">Id of the file to update</param>
        /// <param name="data">Content for the updated file</param>
        void Edit(string id, DTO data);

        /// <summary>
        /// Deletes the file with specified id
        /// </summary>
        /// <param name="id">Id of the file to delete</param>
        void Delete(string id);
    }
}

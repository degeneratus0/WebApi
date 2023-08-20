using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Services.Interfaces
{
    /// <summary>
    /// Defines a contract for database interactions for specified model
    /// </summary>
    /// <typeparam name="T">Model for database interactions</typeparam>
    public interface IRepository<T>
    {
        /// <summary>
        /// Entities of the database table for the model
        /// </summary>
        IQueryable<T> Entities { get; }

        /// <summary>
        /// Gets the model with specified id
        /// </summary>
        /// <param name="id">Id of the model to look for</param>
        Task<T> ReadAsync(int id);

        /// <summary>
        /// Adds the model to the database table
        /// </summary>
        /// <param name="obj">Model to add</param>
        Task AddAsync(T obj);

        /// <summary>
        /// Updates the model with specified id
        /// </summary>
        /// <param name="id">Id of the model to update</param>
        /// <param name="obj">Updated model</param>
        Task EditAsync(int id, T obj);

        /// <summary>
        /// Deletes the model with specified id
        /// </summary>
        /// <param name="id">Id of the model to delete</param>
        Task DeleteAsync(int id);
    }
}

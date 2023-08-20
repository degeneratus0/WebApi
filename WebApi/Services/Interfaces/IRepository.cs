using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Services.Interfaces
{
    public interface IRepository<T>
    {
        IQueryable<T> Entities { get; }
        Task<T> ReadAsync(int id);
        Task AddAsync(T obj);
        Task EditAsync(int id, T obj);
        Task DeleteAsync(int id);
    }
}

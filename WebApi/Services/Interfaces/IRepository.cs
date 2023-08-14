using System.Collections.Generic;

namespace WebApi.Services.Interfaces
{
    public interface IRepository<T>
    {
        T Read(int id);
        IEnumerable<T> ReadAll();
        void Add(T obj);
        void Edit(int id, T obj);
        void Delete(int id);
    }
}

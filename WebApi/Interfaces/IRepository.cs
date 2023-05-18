using System.Collections.Generic;

namespace WebApi.Interfaces
{
    public interface IRepository <T>
    {
        void Set();
        T Read(int id);
        IEnumerable<T> ReadAll();
        void Add(T obj);
        void Edit(int id, T obj);
        void Delete(int id);
    }
}

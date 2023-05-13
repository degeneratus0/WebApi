using System.Collections.Generic;

namespace WebApi.Interfaces
{
    public interface IFile<T, DTO>
    {
        void Set();
        string Read(string id);
        IEnumerable<T> ReadAll();
        void Add(DTO data);
        void Edit(string id, DTO data);
        void Delete(string id);
    }
}

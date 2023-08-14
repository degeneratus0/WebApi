using System.Collections.Generic;

namespace WebApi.Services.Interfaces
{
    public interface IFileRepository<T, DTO>
    {
        void Set(List<DTO> datas);
        void Clear();
        string Read(string id);
        IEnumerable<T> ReadAll();
        void Add(DTO data);
        void Edit(string id, DTO data);
        void Delete(string id);
    }
}

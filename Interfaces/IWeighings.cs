using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Interfaces
{
    interface IWeighings<T, DTO, DTOid>
    {
        void Set();
        DTOid Read(int id);
        IEnumerable<DTOid> ReadAll();
        void Add(DTO obj);
        void Edit(int id, DTO obj);
        void Delete(int id);
        DTO AsDTO(T obj);
        DTOid AsDTOid(T obj);
    }
}

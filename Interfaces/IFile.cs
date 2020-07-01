using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

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

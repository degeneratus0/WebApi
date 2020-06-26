using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Interfaces
{
    interface IFile
    {
        void Set();
        string Read(string id);
        List<Data> ReadAll();
        void Add(DataDTO data);
        void Edit(string id, DataDTO data);
        void Delete(string id);
    }
}
